using DiamondAPI.DTOs.Order;
using DiamondAPI.Models;

namespace DiamondAPI.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrders(Guid CustomerID);

        Task<Order?> GetOrderById(Guid orderId);

        Task<Order?> GetOrderByOrderIDAndCustomerID(Guid orderId, Guid customerID);

        Task<List<Order>> GetOrdersByCusInfos(string? OrderFirstName, string? OrderLastName, string? OrderEmail, string? OrderPhone);

        Task<Order> CreateOrder(Order order);

        Task<Order> UpdateOrder(Guid orderId, UpdateOrderRequestDTO updateOrderRequestDTO);

        Task<Order?> CancelOrder(Guid orderId);

        Task<Order?> ExpiredDepositOrder(Guid orderId); //status for when orders with deposit pending status expires after 1 hour

        Task<bool> DiamondOrdered(Guid? DiamondID);

        Task<IEnumerable<Order>> GetOrdersWithStatus(string status);

        Task<bool> UpdateOrderStatusConfirmationSent(Order order);

        Task<bool> UpdateOrderStatusConfirmed(Order order);

        Task<bool> UpdateOrderStatusPostponed(Order order);

        Task<bool> UpdateOrderStatusCompleted(Order order);

        Task<bool> UpdateOrderStatusDepositReceived(Guid orderID);

        Task<bool> UpdateOrderStatus(Guid OrderID, string Status);

        void UpdateOrder(Order order);

        void Save();

        Task<decimal> GetAmount(Guid OrderID);

        Task<DateTime> GetOrderDate(Guid OrderID);

        Task<List<Order>> GetDepositPendingOrders();
    }
}
