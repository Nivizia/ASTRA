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

        public async Task<Order?> CancelOrder(Guid orderId)
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
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> GetOrderByOrderIDAndCustomerID(Guid orderId, Guid customerID)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId && o.CustomerId == customerID);
        }

        public async Task<List<Order>> GetOrdersByCusInfos(string? orderFirstName = null, string? orderLastName = null, string? orderEmail = null, string? orderPhone = null)
        {
            var query = _context.Orders.Include(o => o.Orderitems).AsQueryable();

            if (!string.IsNullOrEmpty(orderFirstName))
            {
                query = query.Where(o => o.OrderFirstName != null && o.OrderFirstName.Contains(orderFirstName));
            }

            if (!string.IsNullOrEmpty(orderLastName))
            {
                query = query.Where(o => o.OrderLastName != null && o.OrderLastName.Contains(orderLastName));
            }

            if (!string.IsNullOrEmpty(orderEmail))
            {
                query = query.Where(o => o.OrderEmail != null && o.OrderEmail.Contains(orderEmail));
            }

            if (!string.IsNullOrEmpty(orderPhone))
            {
                query = query.Where(o => o.OrderPhone != null && o.OrderPhone.Contains(orderPhone));
            }

            return await query.ToListAsync();
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

        public async Task<bool> UpdateOrderStatusCompleted(Order order)
        {
            order.OrderStatus = "Completed";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOrderStatusConfirmationSent(Order order)
        {
            order.OrderStatus = "Confirmation Sent";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOrderStatusConfirmed(Order order)
        {
            order.OrderStatus = "Confirmed";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOrderStatusPostponed(Order order)
        {
            order.OrderStatus = "Postponed";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> GetAmount(Guid OrderID)
        {
            var amount = await _context.Orders.Where(o => o.OrderId == OrderID).Select(o => o.TotalAmount).FirstOrDefaultAsync();
            if (amount == null)
                return 0;
            return (decimal)amount;
        }

        public async Task<DateTime> GetOrderDate(Guid OrderID)
        {
            var date = await _context.Orders.Where(o => o.OrderId == OrderID).Select(o => o.OrderDate).FirstOrDefaultAsync();
            if (date == null)
                return DateTime.Now;
            return (DateTime)date;
        }

        public async Task<List<Order>> GetDepositPendingOrders()
        {
            return await _context.Orders.Where(o => o.OrderStatus == "Deposit Pending").ToListAsync();
        }

        public async Task<Order?> ExpiredDepositOrder(Guid orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order == null)
                return null;
            order.OrderStatus = "Payment Expired";
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> UpdateOrderStatusDepositReceived(Guid orderID)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderID);
            if (order == null)
                return false;
            order.OrderStatus = "Deposit Received";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOrderStatus(Guid OrderID, string Status)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == OrderID);
            if (order == null)
                return false;
            order.OrderStatus = Status;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
