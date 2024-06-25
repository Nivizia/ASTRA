using DiamondAPI.DTOs.Order;
using DiamondAPI.Models;

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
                OrderDate = createOrderRequestDTO.OrderDate,
                TotalAmount = createOrderRequestDTO.TotalAmount,
                OrderStatus = createOrderRequestDTO.OrderStatus,
            };
        }
    }
}
