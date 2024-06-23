using DiamondAPI.Data;
using DiamondAPI.DTOs.Orderitem;
using DiamondAPI.Interfaces;
using DiamondAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DiamondAPI.Repositories
{
    public class OrderitemRepository : IOrderitemRepository
    {
        private readonly DiamondprojectContext _context;

        public OrderitemRepository(DiamondprojectContext context)
        {
            _context = context;
        }
        public async Task<Orderitem> CreateAsync(Orderitem orderitemModel)
        {
            await _context.Orderitems.AddAsync(orderitemModel);
            await _context.SaveChangesAsync();
            return orderitemModel;
        }

        public Task<Orderitem?> DeleteAsync(Guid OrderitemId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Orderitem>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Orderitem?> GetByIDAsync(Guid OrderitemId)
        {
            throw new NotImplementedException();
        }

        public async Task<Orderitem?> UpdateAsync(Guid OrderitemId, UpdateOrderitemRequestDTO orderitemDTO)
        {
            var existingOrderItem = await _context.Orderitems.FirstOrDefaultAsync(o => o.OrderItemId == OrderitemId);

            if (existingOrderItem != null)
            {
                existingOrderItem.OrderId = orderitemDTO.OrderId;
                existingOrderItem.ProductId = orderitemDTO.ProductId;
                existingOrderItem.Price = orderitemDTO.Price;
                existingOrderItem.ProductType = orderitemDTO.ProductType;

                await _context.SaveChangesAsync();

                return existingOrderItem;
            }
            return null;
        }
    }
}
