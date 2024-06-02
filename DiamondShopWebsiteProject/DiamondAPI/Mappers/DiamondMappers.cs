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

        public static Diamond toDiamondFromCreateDTO(this CreateDiamondRequestDTO diamondDTO)
        {
            return new Diamond
            {
                Name = diamondDTO.Name,
                Price = diamondDTO.Price,
                ImageUrl = diamondDTO.ImageUrl,
                DType = diamondDTO.DType,
                CaratWeight = diamondDTO.CaratWeight,
                Color = diamondDTO.Color,
                Clarity = diamondDTO.Clarity,
                Cut = diamondDTO.Cut
            };
        }
    }
}
