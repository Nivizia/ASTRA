using DiamondAPI.Data;
using DiamondAPI.DTOs.Order;
using DiamondAPI.Interfaces;
using DiamondAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DiamondAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DiamondprojectContext _context;

        public OrderRepository(DiamondprojectContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public Task<Order> DeleteOrder(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Order>> GetAllOrders(Guid CustomerID)
        {
            return await _context.Orders.Include(o => o.Orderitems).Where(o => o.CustomerId == CustomerID).ToListAsync();
        }

        public Task<Order> GetOrderById(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public Task<Order> UpdateOrder(Guid orderId, UpdateOrderRequestDTO updateOrderRequestDTO)
        {
            throw new NotImplementedException();
        }
    }
}
