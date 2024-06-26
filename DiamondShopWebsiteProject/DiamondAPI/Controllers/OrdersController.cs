﻿using DiamondAPI.DTOs.Order;
using DiamondAPI.DTOs.RingPairing;
using DiamondAPI.Interfaces;
using DiamondAPI.Mappers;
using DiamondAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiamondAPI.Controllers
{
    [Route("DiamondAPI/Models/Orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IDiamondRepository _diamondRepo;
        private readonly IRingRepository _ringRepo;
        private readonly IRingPairingRepository _ringPairingRepo;
        private readonly IPendantRepository _pendantRepo;
        private readonly IPendantPairingRepository _pendantPairingRepo;
        private readonly IEarringRepository _earringRepo;
        private readonly IEarringPairingRepository _earringPairingRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly IOrderitemRepository _orderItemRepo;

        public OrdersController(ICustomerRepository customerRepos, IRingPairingRepository ringPairingRepo, IPendantPairingRepository pendantPairingRepo, IOrderRepository orderRepo, IOrderitemRepository orderItemRepo, IRingRepository ringRepo, IPendantRepository pendantRepo, IDiamondRepository diamondRepo, IEarringRepository earringRepo, IEarringPairingRepository earringPairingRepo)
        {
            _customerRepo = customerRepos;
            _diamondRepo = diamondRepo;
            _ringRepo = ringRepo;
            _ringPairingRepo = ringPairingRepo;
            _pendantRepo = pendantRepo;
            _pendantPairingRepo = pendantPairingRepo;
            _earringRepo = earringRepo;
            _earringPairingRepo = earringPairingRepo;
            _orderRepo = orderRepo;
            _orderItemRepo = orderItemRepo;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] CreateOrderRequestDTO createOrderRequestDTO)
        {
            var Customer = await _customerRepo.GetByIDAsync(createOrderRequestDTO.CustomerId);
            if (Customer == null)
                return NotFound("The specified customer could not be found.");

            if (createOrderRequestDTO.Orderitems == null || createOrderRequestDTO.Orderitems.Count == 0)
                return BadRequest("Order items are required and cannot be empty.");

            var Order = createOrderRequestDTO.ToOrderFromCreateDTO();

            List<Orderitem> orderItems = new List<Orderitem>();
            List<Ringpairing> ringPairings = new List<Ringpairing>();
            List<Pendantpairing> pendantPairings = new List<Pendantpairing>();
            List<Earringpairing> earringPairings = new List<Earringpairing>();

            foreach (var orderItem in createOrderRequestDTO.Orderitems)
            {
                if (orderItem.ProductType == "RingPairing")
                {
                    if (await _diamondRepo.IsAvailable(orderItem.CreateRingPairingDTO?.DiamondId))
                        return BadRequest("The diamond was already ordered. ID = " + orderItem.CreateRingPairingDTO?.DiamondId);

                    var ring = await _ringRepo.GetByIDAsync(orderItem.CreateRingPairingDTO?.RingId);
                    if (ring == null)
                        return NotFound("The specified ring could not be found. ID = " + orderItem.CreateRingPairingDTO?.RingId);

                    if (ring.StockQuantity < 1)
                        return BadRequest("The ring is out of stock. ID = " + orderItem.CreateRingPairingDTO?.RingId);

                    ring.StockQuantity--;

                    var diamond = await _diamondRepo.GetByIDAsync(orderItem.CreateRingPairingDTO?.DiamondId);
                    if (diamond == null)
                        return NotFound("The specified diamond could not be found. ID = " + orderItem.CreateRingPairingDTO?.DiamondId);

                    diamond.Available = true;

                    var ringPairing = orderItem.CreateRingPairingDTO?.ToRingPairingFromCreateDTO();
                    if (ringPairing == null)
                        return BadRequest("There was a problem instanciating a ringPairing object.");

                    var modelOrderItem = orderItem.ToOrderItemFromCreateDTO();
                    modelOrderItem.OrderId = Order.OrderId;
                    modelOrderItem.RingPairingId = ringPairing.RProductId;

                    ringPairings.Add(ringPairing);
                    orderItems.Add(modelOrderItem);
                }
                else if (orderItem.ProductType == "PendantPairing")
                {
                    if (await _diamondRepo.IsAvailable(orderItem.CreatePendantPairingDTO?.DiamondId))
                        return BadRequest("The diamond was already ordered. ID = " + orderItem.CreateRingPairingDTO?.DiamondId);

                    var pendant = await _pendantRepo.GetByIDAsync(orderItem.CreatePendantPairingDTO?.PendantId);
                    if (pendant == null)
                        return NotFound("The specified pendant could not be found. ID = " + orderItem.CreatePendantPairingDTO?.PendantId);

                    if (pendant.StockQuantity < 1)
                        return BadRequest("The pendant is out of stock. ID = " + orderItem.CreatePendantPairingDTO?.PendantId);

                    pendant.StockQuantity--;

                    var diamond = await _diamondRepo.GetByIDAsync(orderItem.CreatePendantPairingDTO?.DiamondId);
                    if (diamond == null)
                        return NotFound("The specified diamond could not be found. ID = " + orderItem.CreatePendantPairingDTO?.DiamondId);

                    diamond.Available = true;

                    var pendantPairing = orderItem.CreatePendantPairingDTO?.ToPendantPairingFromCreateDTO();
                    if (pendantPairing == null)
                        return BadRequest("There was a problem instanciating a pendantPairing object.");

                    var modelOrderItem = orderItem.ToOrderItemFromCreateDTO();
                    modelOrderItem.OrderId = Order.OrderId;
                    modelOrderItem.PendantPairingId = pendantPairing.PProductId;

                    pendantPairings.Add(pendantPairing);
                    orderItems.Add(modelOrderItem);
                }
                else if (orderItem.ProductType == "EarringPairing")
                {
                    if (await _diamondRepo.IsAvailable(orderItem.CreateEarringPairingDTO?.DiamondId))
                        return BadRequest("The diamond was already ordered. ID = " + orderItem.CreateRingPairingDTO?.DiamondId);

                    var earring = await _earringRepo.GetByIDAsync(orderItem.CreateEarringPairingDTO?.EarringId);
                    if (earring == null)
                        return NotFound("The specified earring could not be found. ID = " + orderItem.CreateEarringPairingDTO?.EarringId);

                    if (earring.StockQuantity < 1)
                        return BadRequest("The earring is out of stock. ID = " + orderItem.CreateEarringPairingDTO?.EarringId);

                    earring.StockQuantity--;

                    var diamond = await _diamondRepo.GetByIDAsync(orderItem.CreateEarringPairingDTO?.DiamondId);
                    if (diamond == null)
                        return NotFound("The specified diamond could not be found. ID = " + orderItem.CreateEarringPairingDTO?.DiamondId);

                    diamond.Available = true;

                    var earringPairing = orderItem.CreateEarringPairingDTO?.ToEarringPairingFromCreateDTO();
                    if (earringPairing == null)
                        return BadRequest("There was a problem instanciating a earringPairing object.");

                    var modelOrderItem = orderItem.ToOrderItemFromCreateDTO();
                    modelOrderItem.OrderId = Order.OrderId;
                    modelOrderItem.EarringPairingId = earringPairing.EProductId;

                    earringPairings.Add(earringPairing);
                    orderItems.Add(modelOrderItem);
                }
                else if (orderItem.ProductType == "Diamond")
                {
                    if (await _diamondRepo.IsAvailable(orderItem.ProductId))
                        return BadRequest("The diamond is already ordered. ID = " + orderItem.ProductId);

                    var diamond = await _diamondRepo.GetByIDAsync(orderItem.ProductId);
                    if (diamond == null)
                        return NotFound("The specified diamond could not be found. ID = " + orderItem.ProductId);

                    diamond.Available = true;

                    var modelOrderItem = orderItem.ToOrderItemFromCreateDTO();
                    modelOrderItem.OrderId = Order.OrderId;
                    modelOrderItem.DiamondId = orderItem.ProductId;

                    orderItems.Add(modelOrderItem);
                }
                else
                {
                    return BadRequest("Product type not recognised.");
                }
            }
            await _orderRepo.CreateOrder(Order);
            await _ringPairingRepo.CreateRingPairings(ringPairings);
            await _pendantPairingRepo.CreatePendantPairings(pendantPairings);
            await _earringPairingRepo.CreateEarringPairings(earringPairings);
            await _orderItemRepo.CreateOrderItems(orderItems);
            return Ok(Order.ToOrderRequestDTO());
        }

        [HttpGet]
        [Route("{CustomerID}")]
        public async Task<IActionResult> GetOrders([FromRoute] Guid CustomerID)
        {
            var orders = await _orderRepo.GetAllOrders(CustomerID);
            var ordersDTO = orders.Select(o => o.ToOrderRequestDTO()).ToList();
            return Ok(ordersDTO);
        }
    }
}