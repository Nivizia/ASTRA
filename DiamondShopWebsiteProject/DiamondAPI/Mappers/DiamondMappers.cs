using DiamondAPI.DTOs.Diamond;
using DiamondAPI.Models;

namespace DiamondAPI.Mappers
{
    public static class DiamondMapper
    {
        public static DiamondDTO toDiamondDTO(this Diamond diamond)
        {
            return new DiamondDTO
            {
                DProductId = diamond.DProductId,
                Name = diamond.Name,
                Price = diamond.Price,
                ImageUrl = diamond.ImageUrl,
                DType = diamond.DType,
                CaratWeight = diamond.CaratWeight,
                Color = diamond.Color,
                Clarity = diamond.Clarity,
                Cut = diamond.Cut
            };
        }

        public static Diamond toDiamondFromCreateDTO(this CreateDiamondRequestDTO dto)
        {
            return new Diamond
            {
                DProductId = Guid.NewGuid(),
                Name = dto.Name,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
                DType = dto.DType,
                CaratWeight = dto.CaratWeight,
                Color = dto.Color,
                Clarity = dto.Clarity,
                Cut = dto.Cut
            };
        }
    }
}
