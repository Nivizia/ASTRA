using DiamondAPI.DTOs.Order;
using DiamondAPI.Models;
using Microsoft.AspNetCore.StaticFiles;

namespace DiamondAPI.Mappers
{
    public static class OrderMappers
    {
        public static Order ToOrderFromCreateDTO(this CreateOrderRequestDTO createOrderRequestDTO)
        {
            return new Order
            {
                OrderId = Guid.NewGuid(),
                CustomerId = createOrderRequestDTO.CustomerId,
                OrderDate = DateTime.Now,
                TotalAmount = createOrderRequestDTO.TotalAmount,
                OrderStatus = "Received",
            };
        }

        public static OrderRequestDTO ToOrderRequestDTO(this Order order)
        {
            return new OrderRequestDTO
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderStatus = order.OrderStatus
            };
        }
    }
}
