using DiamondAPI.DTOs.Order;
using DiamondAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DiamondAPI.Controllers
{
    [Route("DiamondAPI/Models/Orders")]
    public class OrdersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepo;
        public OrdersController(ICustomerRepository customerRepos)
        {
            _customerRepo = customerRepos;
        }

        [HttpPost]
        [Route("{CustomerID}")]
        public async Task<IActionResult> PlaceOrder([FromQuery] String CustomerID, [FromBody] OrderRequestDTO orderRequest)
        {
            return Ok();
        }
    }
}