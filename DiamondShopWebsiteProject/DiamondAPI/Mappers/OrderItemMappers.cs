using DiamondAPI.DTOs.Order;
using DiamondAPI.DTOs.Orderitem;
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

        public static OrderitemDTO ToOrderitemDTO(this Orderitem orderitem)
        {
            return new OrderitemDTO
            {
                OrderItemId = orderitem.OrderItemId,
                OrderId = orderitem.OrderId,
                DiamondId = orderitem.DiamondId,
                RingPairingId = orderitem.RingPairingId,
                EarringPairingId = orderitem.EarringPairingId,
                PendantPairingId = orderitem.PendantPairingId,
                Price = orderitem.Price,
                ProductType = orderitem.ProductType,
            };
        }
    }
}
