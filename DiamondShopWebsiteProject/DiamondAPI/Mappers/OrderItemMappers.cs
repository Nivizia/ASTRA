using DiamondAPI.DTOs.Order;
using DiamondAPI.Models;

namespace DiamondAPI.Mappers
{
    public static class OrderItemMappers
    {
        public static Orderitem ToOrderItemFromCreateDTO(this CreateOrderitemRequestDTO createOrderitemRequestDTO)
        {
            return new Orderitem
            {
                OrderItemId = Guid.NewGuid(),
                Price = createOrderitemRequestDTO.Price,
                ProductType = createOrderitemRequestDTO.ProductType,
            };
        }
    }
}
