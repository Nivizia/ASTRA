using DiamondAPI.DTOs.Orderitem;
using DiamondAPI.Interfaces;
using DiamondAPI.Mappers;
using DiamondAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiamondAPI.Controllers
{
    [Route("DiamondAPI/Models/OrderItems")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderitemRepository _orderItemRepository;

        public OrderItemController(IOrderitemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orderitem>>> GetOrderItems()
        {
            var orders = await _orderItemRepository.GetAllAsync();
            var ordersDTO = orders.Select(o => o.ToOrderitemDTO());
            return Ok(ordersDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Orderitem>> GetOrderItem(Guid id)
        {
            var orderItem = await _orderItemRepository.GetByIDAsync(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            return orderItem;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderItem(Guid id, UpdateOrderitemRequestDTO orderitemDTO)
        {
            var orderItem = await _orderItemRepository.UpdateAsync(id, orderitemDTO);

            if (orderItem == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Orderitem>> PostOrderItem(Orderitem orderitem)
        {
            await _orderItemRepository.CreateAsync(orderitem);

            return CreatedAtAction("GetOrderItem", new { id = orderitem.OrderItemId }, orderitem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(Guid id)
        {
            var orderItem = await _orderItemRepository.DeleteAsync(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
