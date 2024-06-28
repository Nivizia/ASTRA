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
        private readonly IOrderRepository _orderRepo;
        private readonly IOrderitemRepository _orderItemRepo;

        public OrdersController(ICustomerRepository customerRepos, IRingPairingRepository ringPairingRepo, IPendantPairingRepository pendantPairingRepo, IOrderRepository orderRepo, IOrderitemRepository orderItemRepo, IRingRepository ringRepo, IPendantRepository pendantRepo, IDiamondRepository diamondRepo)
        {
            _customerRepo = customerRepos;
            _diamondRepo = diamondRepo;
            _ringRepo = ringRepo;
            _ringPairingRepo = ringPairingRepo;
            _pendantRepo = pendantRepo;
            _pendantPairingRepo = pendantPairingRepo;
            _orderRepo = orderRepo;
            _orderItemRepo = orderItemRepo;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] CreateOrderRequestDTO createOrderRequestDTO)
        {
            var Order = createOrderRequestDTO.ToOrderFromCreateDTO();
            await _orderRepo.CreateOrder(Order);

            foreach (var orderItem in createOrderRequestDTO.Orderitems)
            {
                if (orderItem.ProductType == "RingPairing")
                {

                    var ring = await _ringRepo.GetByIDAsync(orderItem.CreateRingPairingDTO?.RingId);
                    if (ring == null)
                        return NotFound();

                    var diamond = await _diamondRepo.GetByIDAsync(orderItem.CreateRingPairingDTO?.DiamondId);
                    if (diamond == null)
                        return NotFound();

                    var ringPairing = orderItem.CreateRingPairingDTO?.ToRingPairingFromCreateDTO();

                    if (ringPairing == null)
                        return BadRequest();

                    var modelOrderItem = orderItem.ToOrderItemFromCreateDTO();
                    modelOrderItem.OrderId = Order.OrderId;
                    modelOrderItem.RingPairingId = ringPairing.RProductId;

                    await _orderItemRepo.CreateAsync(modelOrderItem);
                    await _ringPairingRepo.CreateAsync(ringPairing);
                }
                else if (orderItem.ProductType == "PendantPairing")
                {
                    var pendant = await _pendantRepo.GetByIDAsync(orderItem.CreatePendantPairingDTO?.PendantId);
                    if (pendant == null)
                        return NotFound();

                    var diamond = await _diamondRepo.GetByIDAsync(orderItem.CreatePendantPairingDTO?.DiamondId);
                    if (diamond == null)
                        return NotFound();

                    var pendantPairing = orderItem.CreatePendantPairingDTO?.ToPendantPairingFromCreateDTO();

                    if (pendantPairing == null)
                        return BadRequest();

                    var modelOrderItem = orderItem.ToOrderItemFromCreateDTO();
                    modelOrderItem.OrderId = Order.OrderId;
                    modelOrderItem.PendantPairingId = pendantPairing.PProductId;

                    await _orderItemRepo.CreateAsync(modelOrderItem);
                    await _pendantPairingRepo.AddPendantPairingAsync(pendantPairing);
                }
                else if (orderItem.ProductType == "Diamond")
                {
                    var diamond = await _diamondRepo.GetByIDAsync(orderItem.ProductId);
                    if (diamond == null)
                        return NotFound();

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