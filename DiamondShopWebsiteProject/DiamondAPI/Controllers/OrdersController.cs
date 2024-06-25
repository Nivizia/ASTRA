using DiamondAPI.DTOs.Order;
using DiamondAPI.Interfaces;
using DiamondAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace DiamondAPI.Controllers
{
    [Route("DiamondAPI/Models/Orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IRingPairingRepository _ringPairingRepo;
        private readonly IPendantPairingRepository _pendantPairingRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly IOrderitemRepository _orderItemRepo;

        public OrdersController(ICustomerRepository customerRepos, IRingPairingRepository ringPairingRepo, IPendantPairingRepository pendantPairingRepo, IOrderRepository orderRepo, IOrderitemRepository orderItemRepo)
        {
            _customerRepo = customerRepos;
            _ringPairingRepo = ringPairingRepo;
            _pendantPairingRepo = pendantPairingRepo;
            _orderRepo = orderRepo;
            _orderItemRepo = orderItemRepo;
        }

        [HttpPost]
        [Route("{CustomerID}")]
        public async Task<IActionResult> PlaceOrder([FromRoute] String CustomerID, [FromBody] CreateOrderRequestDTO createOrderRequestDTO)
        {
            var Order = createOrderRequestDTO.ToOrderFromCreateDTO();
            await _orderRepo.CreateOrder(Order);

            foreach (var orderItem in createOrderRequestDTO.Orderitems)
            {
                if (orderItem.ProductType == "RingPairing")
                {
                    var ringPairing = orderItem.CreateRingPairingDTO?.ToRingPairingFromCreateDTO();

                    if (ringPairing == null)
                        return BadRequest();

                    var modelOrderItem = orderItem.ToOrderItemFromCreateDTO();
                    modelOrderItem.OrderId = Order.OrderId;
                    modelOrderItem.ProductId = ringPairing.RProductId;

                    await _orderItemRepo.CreateAsync(modelOrderItem);
                    await _ringPairingRepo.CreateAsync(ringPairing);
                }
                else if (orderItem.ProductType == "PendantPairing")
                {
                    var pendantPairing = orderItem.CreatePendantPairingDTO?.ToPendantPairingFromCreateDTO();

                    if (pendantPairing == null)
                        return BadRequest();

                    var modelOrderItem = orderItem.ToOrderItemFromCreateDTO();
                    modelOrderItem.OrderId = Order.OrderId;
                    modelOrderItem.ProductId = pendantPairing.PProductId;

                    await _orderItemRepo.CreateAsync(modelOrderItem);
                    await _pendantPairingRepo.AddPendantPairingAsync(pendantPairing);
                }
                else if (orderItem.ProductType == "Diamond")
                {
                    var modelOrderItem = orderItem.ToOrderItemFromCreateDTO();
                    modelOrderItem.OrderId = Order.OrderId;
                    modelOrderItem.ProductId = orderItem.ProductId;

                    await _orderItemRepo.CreateAsync(modelOrderItem);
                }
                else
                {
                    return BadRequest();
                }
            }
            return Ok(Order.ToOrderRequestDTO());
        }
    }
}