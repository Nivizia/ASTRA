using DiamondAPI.DTOs.Order;
using DiamondAPI.Models;

namespace DiamondAPI.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderById(Guid orderId);

        Task<Order> CreateOrder(CreateOrderRequestDTO createOrderRequestDTO);

        Task<Order> UpdateOrder(Guid orderId, UpdateOrderRequestDTO updateOrderRequestDTO);

        Task<Order> DeleteOrder(Guid orderId);
    }
}
