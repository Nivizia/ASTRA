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
                RingTypeId = GetRingTypeIdFromName(ringDTO.RingType),
                RingSubtypeId = GetRingSubtypeIdFromName(ringDTO.RingSubtype),
                FrameTypeId = GetFrameTypeIdFromName(ringDTO.FrameType),
                MetalTypeId = GetMetalTypeIdFromName(ringDTO.MetalType),
                RingSize = ringDTO.RingSize,
                RingName = ringDTO.RingName,
                Price = ringDTO.Price,
                StockQuantity = ringDTO.StockQuantity,
                ImageUrl = ringDTO.ImageUrl,
            };
        }
        private static Guid GetRingTypeIdFromName(string? ringTypeName)
        {
            using (var context = new DiamondprojectContext())
            {
                var ringType = context.Ringtypes
                    .FirstOrDefault(rt => rt.TypeName == ringTypeName);

                if (ringType == null)
                {
                    throw new ArgumentException($"Ring type with name {ringTypeName} not found.");
                }

                return ringType.RingTypeId;
            }
        }

        private static Guid GetRingSubtypeIdFromName(string? ringSubtypeName)
        {
            using (var context = new DiamondprojectContext())
            {
                var ringSubtype = context.Ringsubtypes
                    .FirstOrDefault(rs => rs.SubtypeName == ringSubtypeName);

                if (ringSubtype == null)
                {
                    throw new ArgumentException($"Ring subtype with name {ringSubtypeName} not found.");
                }

                return ringSubtype.RingSubtypeId;
            }
        }

        private static Guid GetFrameTypeIdFromName(string? frameTypeName)
        {
            using (var context = new DiamondprojectContext())
            {
                var frameType = context.Frametypes
                    .FirstOrDefault(ft => ft.FrameTypeName == frameTypeName);

                if (frameType == null)
                {
                    throw new ArgumentException($"Frame type with name {frameTypeName} not found.");
                }

                return frameType.FrameTypeId;
            }
        }

        private static Guid GetMetalTypeIdFromName(string? metalTypeName)
        {
            using (var context = new DiamondprojectContext())
            {
                var metalType = context.Metaltypes
                    .FirstOrDefault(mt => mt.MetalTypeName == metalTypeName);

                if (metalType == null)
                {
                    throw new ArgumentException($"Metal type with name {metalTypeName} not found.");
                }

                return metalType.MetalTypeId;
            }
        }
    }
}
