﻿using DiamondAPI.Data;
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

        public async Task<Order?> DeleteOrder(Guid orderId)
        {
            var Order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (Order == null)
                return null;
            Order.OrderStatus = "Cancelled";
            await _context.SaveChangesAsync();
            return Order;
        }

        public async Task<bool> DiamondOrdered(Guid? DiamondID)
        {
            if (DiamondID == null)
            {
                return false;
            }

            var diamondOrderedAlone = await _context.Orderitems.AnyAsync(o => o.DiamondId == DiamondID);
            var diamondOrderedInEarringPairing = await _context.Earringpairings.AnyAsync(e => e.DiamondId == DiamondID);
            var diamondOrderedInRingPairing = await _context.Ringpairings.AnyAsync(r => r.DiamondId == DiamondID);
            var diamondOrderedInPendantPairing = await _context.Pendantpairings.AnyAsync(p => p.DiamondId == DiamondID);
            return diamondOrderedAlone || diamondOrderedInEarringPairing || diamondOrderedInRingPairing || diamondOrderedInPendantPairing;
        }

        public async Task<List<Order>> GetAllOrders(Guid CustomerID)
        {
            return await _context.Orders.Include(o => o.Orderitems).Where(o => o.CustomerId == CustomerID).ToListAsync();
        }

        public async Task<Order?> GetOrderById(Guid orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order == null)
                return null;
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<IEnumerable<Order>> GetOrdersWithStatus(string status)
        {
            return await _context.Orders.Where(o => o.OrderStatus == status).ToListAsync();
        }

        public async void Save()
        {
            await _context.SaveChangesAsync();
        }

        public Task<Order> UpdateOrder(Guid orderId, UpdateOrderRequestDTO updateOrderRequestDTO)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
        }

        public async Task<bool> UpdateOrderStatus(Order order)
        {
            order.OrderStatus = "Confirmed";
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
