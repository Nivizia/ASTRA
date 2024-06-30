using DiamondAPI.DTOs.Order;
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
            if (createOrderRequestDTO.Orderitems == null || createOrderRequestDTO.Orderitems.Count == 0)
                return BadRequest("Order items are required and cannot be empty.");

            var Order = createOrderRequestDTO.ToOrderFromCreateDTO();

            foreach (var orderItem in createOrderRequestDTO.Orderitems)
            {
                if (orderItem.ProductType == "RingPairing")
                {
                    if (await _orderRepo.DiamondOrdered(orderItem.CreateRingPairingDTO?.DiamondId))
                        return BadRequest("The diamond is already ordered.");

                    var ring = await _ringRepo.GetByIDAsync(orderItem.CreateRingPairingDTO?.RingId);
                    if (ring == null)
                        return NotFound("The specified ring could not be found.");

                    var diamond = await _diamondRepo.GetByIDAsync(orderItem.CreateRingPairingDTO?.DiamondId);
                    if (diamond == null)
                        return NotFound("The specified diamond could not be found.");

                    var ringPairing = orderItem.CreateRingPairingDTO?.ToRingPairingFromCreateDTO();
                    if (ringPairing == null)
                        return BadRequest("There was a problem instanciating a ringPairing object.");


                    await _orderRepo.CreateOrder(Order);

                    var modelOrderItem = orderItem.ToOrderItemFromCreateDTO();
                    modelOrderItem.OrderId = Order.OrderId;
                    modelOrderItem.RingPairingId = ringPairing.RProductId;

                    await _ringPairingRepo.CreateAsync(ringPairing);
                    await _orderItemRepo.CreateAsync(modelOrderItem);
                }
                else if (orderItem.ProductType == "PendantPairing")
                {
                    if (await _orderRepo.DiamondOrdered(orderItem.CreatePendantPairingDTO?.DiamondId))
                        return BadRequest("The diamond is already ordered.");

                    var pendant = await _pendantRepo.GetByIDAsync(orderItem.CreatePendantPairingDTO?.PendantId);
                    if (pendant == null)
                        return NotFound("The specified pendant could not be found.");

                    var diamond = await _diamondRepo.GetByIDAsync(orderItem.CreatePendantPairingDTO?.DiamondId);
                    if (diamond == null)
                        return NotFound("The specified diamond could not be found.");

                    var pendantPairing = orderItem.CreatePendantPairingDTO?.ToPendantPairingFromCreateDTO();
                    if (pendantPairing == null)
                        return BadRequest("There was a problem instanciating a pendantPairing object.");

                    await _orderRepo.CreateOrder(Order);

                    var modelOrderItem = orderItem.ToOrderItemFromCreateDTO();
                    modelOrderItem.OrderId = Order.OrderId;
                    modelOrderItem.PendantPairingId = pendantPairing.PProductId;

                    await _pendantPairingRepo.AddPendantPairingAsync(pendantPairing);
                    await _orderItemRepo.CreateAsync(modelOrderItem);
                }
                else if (orderItem.ProductType == "EarringPairing")
                {
                    if (await _orderRepo.DiamondOrdered(orderItem.CreateEarringPairingDTO?.DiamondId))
                        return BadRequest("The diamond is already ordered.");

                    var earring = await _earringRepo.GetByIDAsync(orderItem.CreateEarringPairingDTO?.EarringId);
                    if (earring == null)
                        return NotFound("The specified earring could not be found.");

                    var diamond = await _diamondRepo.GetByIDAsync(orderItem.CreateEarringPairingDTO?.DiamondId);
                    if (diamond == null)
                        return NotFound("The specified diamond could not be found.");

                    var earringPairing = orderItem.CreateEarringPairingDTO?.ToEarringPairingFromCreateDTO();
                    if (earringPairing == null)
                        return BadRequest("There was a problem instanciating a earringPairing object.");

                    await _orderRepo.CreateOrder(Order);

                    var modelOrderItem = orderItem.ToOrderItemFromCreateDTO();
                    modelOrderItem.OrderId = Order.OrderId;
                    modelOrderItem.EarringPairingId = earringPairing.EProductId;

                    await _earringPairingRepo.CreateAsync(earringPairing);
                    await _orderItemRepo.CreateAsync(modelOrderItem);
                }
                else if (orderItem.ProductType == "Diamond")
                {
                    if (await _orderRepo.DiamondOrdered(orderItem.ProductId))
                        return BadRequest("The diamond is already ordered.");

                    var diamond = await _diamondRepo.GetByIDAsync(orderItem.ProductId);
                    if (diamond == null)
                        return NotFound("The specified diamond could not be found.");

                    await _orderRepo.CreateOrder(Order);

                    var modelOrderItem = orderItem.ToOrderItemFromCreateDTO();
                    modelOrderItem.OrderId = Order.OrderId;
                    modelOrderItem.DiamondId = orderItem.ProductId;

                    await _orderItemRepo.CreateAsync(modelOrderItem);
                }
                else
                {
                    return BadRequest();
                }
            }
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