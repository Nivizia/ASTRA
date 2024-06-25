using DiamondAPI.DTOs.Order;
using DiamondAPI.Interfaces;
using DiamondAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace DiamondAPI.Controllers
{
    [Route("DiamondAPI/Models/Orders")]
    public class OrdersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IRingPairingRepository _ringPairingRepo;
        private readonly IPendantPairingRepository _pendantPairingRepo;
        private readonly IOrderRepository _orderRepo;

        public OrdersController(ICustomerRepository customerRepos, IRingPairingRepository ringPairingRepo, IPendantPairingRepository pendantPairingRepo, IOrderRepository orderRepo)
        {
            _customerRepo = customerRepos;
            _ringPairingRepo = ringPairingRepo;
            _pendantPairingRepo = pendantPairingRepo;
            _orderRepo = orderRepo;
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

                    await _ringPairingRepo.Create(ringPairing);
                }
                else if (orderItem.ProductType == "PendantPairing")
                {
                    var pendantPairing = orderItem.CreatePendantPairingDTO?.ToPendantPairingFromCreateDTO();

                    if (pendantPairing == null)
                        return BadRequest();

                    await _pendantPairingRepo.AddPendantPairingAsync(pendantPairing);
                }
                else
                {
                    return BadRequest();
                }
            }
            return Ok();
        }
    }
}