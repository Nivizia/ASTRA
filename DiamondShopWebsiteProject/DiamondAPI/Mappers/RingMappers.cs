using DiamondAPI.DTOs.Ring;
using DiamondAPI.Models;

namespace DiamondAPI.Mappers
{
    public static class RingMappers
    {
        public static RingDTO toRingDTO(this Ring ring)
        {
            return new RingDTO
            {
                RingId = ring.RingId,
                Name = ring.Name,
                Price = ring.Price,
                StockQuantity = ring.StockQuantity,
                ImageUrl = ring.ImageUrl,
                MetalType = ring.MetalType,
                RingSize = ring.RingSize
            };
        }

        public static Ring toRingFromCreateDTO(this CreateRingRequestDTO ringDTO)
        {
            return new Ring
            {
                RingId = Guid.NewGuid(),
                Name = ringDTO.Name,
                Price = ringDTO.Price,
                StockQuantity = ringDTO.StockQuantity,
                ImageUrl = ringDTO.ImageUrl,
                MetalType = ringDTO.MetalType,
                RingSize = ringDTO.RingSize
            };
        }
    }
}
