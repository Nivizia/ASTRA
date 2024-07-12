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

        public async Task<List<Orderitem>> CreateOrderItems(List<Orderitem> orderitems)
        {
            await _context.Orderitems.AddRangeAsync(orderitems);
            await _context.SaveChangesAsync();
            return orderitems;
        }

        public async Task<Orderitem?> DeleteAsync(Guid OrderitemId)
        {
            var orderitemModel = await _context.Orderitems.FirstOrDefaultAsync(o => o.OrderItemId == OrderitemId);
            if (orderitemModel == null)
            {
                return null;
            }
            _context.Orderitems.Remove(orderitemModel);
            await _context.SaveChangesAsync();
            return orderitemModel;
        }

        public async Task<List<Orderitem>> GetAllAsync()
        {
            return await _context.Orderitems.ToListAsync();
        }

        public async Task<Orderitem?> GetByIDAsync(Guid OrderitemId)
        {
            return await _context.Orderitems.FindAsync(OrderitemId);
        }

        public async Task<List<Orderitem>> GetOrderitemsByOrderId(Guid orderId)
        {
            return await _context.Orderitems.Where(o => o.OrderId == orderId).ToListAsync();
        }

        public async Task<Orderitem?> UpdateAsync(Guid OrderitemId, UpdateOrderitemRequestDTO orderitemDTO)
        {
            var existingOrderItem = await _context.Orderitems.FirstOrDefaultAsync(o => o.OrderItemId == OrderitemId);

            if (existingOrderItem != null)
            {
                existingOrderItem.OrderId = orderitemDTO.OrderId;
                existingOrderItem.DiamondId = orderitemDTO.DiamondId;
                existingOrderItem.RingPairingId = orderitemDTO.RingPairingId;
                existingOrderItem.EarringPairingId = orderitemDTO.EarringPairingId;
                existingOrderItem.PendantPairingId = orderitemDTO.PendantPairingId;
                existingOrderItem.Price = orderitemDTO.Price;
                existingOrderItem.ProductType = orderitemDTO.ProductType;

                await _context.SaveChangesAsync();

                return existingOrderItem;
            }
            return null;
        }
    }
}
