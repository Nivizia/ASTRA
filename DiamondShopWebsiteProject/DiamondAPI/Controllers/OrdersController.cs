using DiamondAPI.DTOs.Order;
using DiamondAPI.DTOs.RingPairing;
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
        public OrdersController(ICustomerRepository customerRepos, IRingPairingRepository ringPairingRepo, IPendantPairingRepository pendantPairingRepo)
        {
            _customerRepo = customerRepos;
            _ringPairingRepo = ringPairingRepo;
            _pendantPairingRepo = pendantPairingRepo;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromQuery] String CustomerID, [FromBody] OrderRequestDTO orderRequest)
        {
            foreach (var orderItem in orderRequest.Orderitems)
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
