using DiamondAPI.Data;
using DiamondAPI.DTOs.Ring;
using DiamondAPI.Models;

namespace DiamondAPI.Mappers
{
    public static class RingMappers
    {
        public static RingDTO ToRingDTO(this Ring ring)
        {
            return new RingDTO
            {
                RingId = ring.RingId,
                RingType = ring.RingType?.TypeName,
                RingSubtype = ring.RingSubtype?.SubtypeName,
                FrameType = ring.FrameType?.FrameTypeName,
                MetalType = ring.MetalType?.MetalTypeName,
                StoneCut = ring.StoneCut?.StoneCutName,
                SpecialFeature = ring.SpecialFeature?.FeatureDescription,
                RingSize = ring.RingSize,
                RingName = ring.RingName,
                Price = ring.Price,
                StockQuantity = ring.StockQuantity,
                ImageUrl = ring.ImageUrl,
                Shapes = ring.Ringshapedetails.Select(r => r.Shape?.ShapeName).ToList()
            };
        }

        public static Ring toRingFromCreateDTO(this CreateRingRequestDTO ringDTO)
        {
            return new Ring
            {
                RingId = Guid.NewGuid(),
                RingSize = ringDTO.RingSize,
                RingName = ringDTO.RingName,
                Price = ringDTO.Price,
                StockQuantity = ringDTO.StockQuantity,
                ImageUrl = ringDTO.ImageUrl,
            };
        }
    }
}
