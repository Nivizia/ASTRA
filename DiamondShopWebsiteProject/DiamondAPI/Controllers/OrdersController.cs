using DiamondAPI.DTOs.Order;
using DiamondAPI.DTOs.RingPairing;
using DiamondAPI.Interfaces;
using DiamondAPI.Mappers;
using DiamondAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        private readonly EmailService _emailService;

        public OrdersController(ICustomerRepository customerRepos, IRingPairingRepository ringPairingRepo, IPendantPairingRepository pendantPairingRepo, IOrderRepository orderRepo, IOrderitemRepository orderItemRepo, IRingRepository ringRepo, IPendantRepository pendantRepo, IDiamondRepository diamondRepo, IEarringRepository earringRepo, IEarringPairingRepository earringPairingRepo, EmailService emailService)
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
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] CreateOrderRequestDTO createOrderRequestDTO)
        {
            var customer = await _customerRepo.GetByIDAsync(createOrderRequestDTO.CustomerId);
            if (customer == null)
                return NotFound("User could not be found.");

            if (createOrderRequestDTO.Orderitems == null || createOrderRequestDTO.Orderitems.Count == 0)
                return BadRequest("Order items are required and cannot be empty.");

            var order = createOrderRequestDTO.ToOrderFromCreateDTO();

            List<Orderitem> orderItems = new List<Orderitem>();
            List<Ringpairing> ringPairings = new List<Ringpairing>();
            List<Pendantpairing> pendantPairings = new List<Pendantpairing>();
            List<Earringpairing> earringPairings = new List<Earringpairing>();

            foreach (var orderItem in createOrderRequestDTO.Orderitems)
            {
                var productType = orderItem.ProductType?.ToLower();

                if (productType == null)
                    return BadRequest("Product type is required.");

                if (productType == "ringpairing")
                {
                    var ring = await _ringRepo.GetByIDAsync(orderItem.CreateRingPairingDTO?.RingId);
                    if (ring == null)
                        return NotFound("The specified ring could not be found. ID = " + orderItem.CreateRingPairingDTO?.RingId);

                    var isAvailable = await _diamondRepo.IsAvailable(orderItem.ProductId);
                    if (!isAvailable) return BadRequest("The diamond is already ordered. ID = " + orderItem.ProductId);

                    if (ring.StockQuantity < 1)
                        return BadRequest("The ring is out of stock. ID = " + orderItem.CreateRingPairingDTO?.RingId);

                    ring.StockQuantity--;

                    var diamond = await _diamondRepo.GetByIDAsync(orderItem.CreateRingPairingDTO?.DiamondId);
                    if (diamond == null)
                        return NotFound("The specified diamond could not be found. ID = " + orderItem.CreateRingPairingDTO?.DiamondId);

                    diamond.Available = false;

                    var ringPairing = orderItem.CreateRingPairingDTO?.ToRingPairingFromCreateDTO();
                    if (ringPairing == null)
                        return BadRequest("There was a problem instantiating a ringPairing object.");

                    var modelOrderItem = orderItem.ToOrderItemFromCreateDTO();
                    modelOrderItem.OrderId = order.OrderId;
                    modelOrderItem.RingPairingId = ringPairing.RProductId;

                    ringPairings.Add(ringPairing);
                    orderItems.Add(modelOrderItem);
                }
                else if (productType == "pendantpairing")
                {
                    var pendant = await _pendantRepo.GetByIDAsync(orderItem.CreatePendantPairingDTO?.PendantId);
                    if (pendant == null)
                        return NotFound("The specified pendant could not be found. ID = " + orderItem.CreatePendantPairingDTO?.PendantId);

                    var isAvailable = await _diamondRepo.IsAvailable(orderItem.ProductId);
                    if (!isAvailable) return BadRequest("The diamond is already ordered. ID = " + orderItem.ProductId);

                    if (pendant.StockQuantity < 1)
                        return BadRequest("The pendant is out of stock. ID = " + orderItem.CreatePendantPairingDTO?.PendantId);

                    pendant.StockQuantity--;

                    var diamond = await _diamondRepo.GetByIDAsync(orderItem.CreatePendantPairingDTO?.DiamondId);
                    if (diamond == null)
                        return NotFound("The specified diamond could not be found. ID = " + orderItem.CreatePendantPairingDTO?.DiamondId);

                    diamond.Available = false;

                    var pendantPairing = orderItem.CreatePendantPairingDTO?.ToPendantPairingFromCreateDTO();
                    if (pendantPairing == null)
                        return BadRequest("There was a problem instantiating a pendantPairing object.");

                    var modelOrderItem = orderItem.ToOrderItemFromCreateDTO();
                    modelOrderItem.OrderId = order.OrderId;
                    modelOrderItem.PendantPairingId = pendantPairing.PProductId;

                    pendantPairings.Add(pendantPairing);
                    orderItems.Add(modelOrderItem);
                }
                else if (productType == "earringpairing")
                {
                    var earring = await _earringRepo.GetByIDAsync(orderItem.CreateEarringPairingDTO?.EarringId);
                    if (earring == null)
                        return NotFound("The specified earring could not be found. ID = " + orderItem.CreateEarringPairingDTO?.EarringId);

                    var isAvailable = await _diamondRepo.IsAvailable(orderItem.ProductId);
                    if (!isAvailable) return BadRequest("The diamond is already ordered. ID = " + orderItem.ProductId);

                    if (earring.StockQuantity < 1)
                        return BadRequest("The earring is out of stock. ID = " + orderItem.CreateEarringPairingDTO?.EarringId);

                    earring.StockQuantity--;

                    var diamond = await _diamondRepo.GetByIDAsync(orderItem.CreateEarringPairingDTO?.DiamondId);
                    if (diamond == null)
                        return NotFound("The specified diamond could not be found. ID = " + orderItem.CreateEarringPairingDTO?.DiamondId);

                    diamond.Available = false;

                    var earringPairing = orderItem.CreateEarringPairingDTO?.ToEarringPairingFromCreateDTO();
                    if (earringPairing == null)
                        return BadRequest("There was a problem instantiating an earringPairing object.");

                    var modelOrderItem = orderItem.ToOrderItemFromCreateDTO();
                    modelOrderItem.OrderId = order.OrderId;
                    modelOrderItem.EarringPairingId = earringPairing.EProductId;

                    earringPairings.Add(earringPairing);
                    orderItems.Add(modelOrderItem);
                }
                else if (productType == "diamond")
                {
                    var diamond = await _diamondRepo.GetByIDAsync(orderItem.ProductId);
                    if (diamond == null)
                        return NotFound("The specified diamond could not be found. ID = " + orderItem.ProductId);

                    var isAvailable = await _diamondRepo.IsAvailable(orderItem.ProductId);
                    if (!isAvailable) return BadRequest("The diamond is already ordered. ID = " + orderItem.ProductId);

                    diamond.Available = false;

                    var modelOrderItem = orderItem.ToOrderItemFromCreateDTO();
                    modelOrderItem.OrderId = order.OrderId;
                    modelOrderItem.DiamondId = orderItem.ProductId;

                    orderItems.Add(modelOrderItem);
                }
                else
                {
                    return BadRequest("Product type not recognized.");
                }
            }

            await _orderRepo.CreateOrder(order);
            await _ringPairingRepo.CreateRingPairings(ringPairings);
            await _pendantPairingRepo.CreatePendantPairings(pendantPairings);
            await _earringPairingRepo.CreateEarringPairings(earringPairings);
            await _orderItemRepo.CreateOrderItems(orderItems);
            return Ok(order.ToOrderRequestDTO());
        }

        [HttpGet]
        [Route("OrderHistory/{CustomerID}")]
        public async Task<IActionResult> GetOrders([FromRoute] Guid CustomerID)
        {
            var orders = await _orderRepo.GetAllOrdersForCustomer(CustomerID);
            return Ok(orders.ToOrderRequestDTOs());
        }

        [HttpDelete]
        [Route("CancelOrder")]
        public async Task<IActionResult> CancelOrder([FromQuery] string orderid, [FromQuery] string token)
        {
            var (isValid, errorMessage) = TokenHelper.ValidateToken(token);

            if (!isValid)
            {
                return Unauthorized(errorMessage);
            }

            Guid orderIdGuid;
            if (!Guid.TryParse(orderid, out orderIdGuid))
                return BadRequest("Invalid order ID format.");

            var order = await _orderRepo.GetOrderByIdAsync(orderIdGuid);
            if (order == null)
                return NotFound("Order not found.");

            await _orderRepo.UpdateOrderStatus(orderid, "Cancelled", token);

            var orderItems = await _orderItemRepo.GetOrderItemsByOrderId(orderid);

            foreach (var item in orderItems)
            {
                if (item.ProductType == "diamond")
                {
                    var diamond = await _diamondRepo.GetByIDAsync(item.ProductId);
                    if (diamond != null)
                    {
                        diamond.Available = true;
                        await _diamondRepo.UpdateDiamondAsync(diamond);
                    }
                }
                else if (item.ProductType == "ringpairing")
                {
                    var ring = await _ringRepo.GetByIDAsync(item.RingId);
                    if (ring != null)
                    {
                        ring.StockQuantity++;
                        await _ringRepo.UpdateRingAsync(ring);
                    }

                    var diamond = await _diamondRepo.GetByIDAsync(item.DiamondId);
                    if (diamond != null)
                    {
                        diamond.Available = true;
                        await _diamondRepo.UpdateDiamondAsync(diamond);
                    }
                }
                else if (item.ProductType == "pendantpairing")
                {
                    var pendant = await _pendantRepo.GetByIDAsync(item.PendantId);
                    if (pendant != null)
                    {
                        pendant.StockQuantity++;
                        await _pendantRepo.UpdatePendantAsync(pendant);
                    }

                    var diamond = await _diamondRepo.GetByIDAsync(item.DiamondId);
                    if (diamond != null)
                    {
                        diamond.Available = true;
                        await _diamondRepo.UpdateDiamondAsync(diamond);
                    }
                }
                else if (item.ProductType == "earringpairing")
                {
                    var earring = await _earringRepo.GetByIDAsync(item.EarringId);
                    if (earring != null)
                    {
                        earring.StockQuantity++;
                        await _earringRepo.UpdateEarringAsync(earring);
                    }

                    var diamond = await _diamondRepo.GetByIDAsync(item.DiamondId);
                    if (diamond != null)
                    {
                        diamond.Available = true;
                        await _diamondRepo.UpdateDiamondAsync(diamond);
                    }
                }
            }

            return Ok("Order cancelled successfully.");
        }

        [HttpPut]
        [Route("ConfirmOrder")]
        public async Task<IActionResult> UpdateOrderStatus([FromQuery] string orderid, [FromQuery] string token)
        {
            var (isValid, errorMessage) = TokenHelper.ValidateToken(token);

            if (!isValid)
            {
                return Unauthorized(errorMessage);
            }

            await _orderRepo.UpdateOrderStatus(orderid, "Confirmed", token);

            var order = await _orderRepo.GetOrderByIdAsync(Guid.Parse(orderid));
            if (order == null)
                return NotFound("Order not found.");

            if (!string.IsNullOrEmpty(order.Customer.Email))
            {
                await _emailService.SendOrderConfirmationEmail(order.Customer.Email, order.OrderId.ToString());
            }

            return Ok("Order status updated successfully.");
        }
    }
}
