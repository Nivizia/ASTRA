﻿using DiamondAPI.DTOs.Order;
using DiamondAPI.Models;

namespace DiamondAPI.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrders(Guid CustomerID);

        Task<Order?> GetOrderById(Guid orderId);

        Task<Order> CreateOrder(Order order);

        Task<Order> UpdateOrder(Guid orderId, UpdateOrderRequestDTO updateOrderRequestDTO);

        Task<Order?> DeleteOrder(Guid orderId);

        Task<bool> DiamondOrdered(Guid? DiamondID);

        Task<IEnumerable<Order>> GetOrdersWithStatus(string status);

        Task<bool> UpdateOrderStatusConfirmationSent(Order order);

        Task<bool> UpdateOrderStatusConfirmed(Order order);

        Task<bool> UpdateOrderStatusPostponed(Order order);

        Task<bool> UpdateOrderStatusCompleted(Order order);

        void UpdateOrder(Order order);

        void Save();
    }
}
