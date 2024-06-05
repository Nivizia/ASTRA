using DiamondAPI.DTOs.Earring;
using DiamondAPI.Models;

namespace DiamondAPI.Mappers
{
    public static class EarringMappers
    {
        public static EarringDTO toEarringDTO(this Earring earring)
        {
            return new EarringDTO
            {
                EarringId = earring.EarringId,
                Name = earring.Name,
                Price = earring.Price,
                StockQuantity = earring.StockQuantity,
                ImageUrl = earring.ImageUrl,
                MetalType = earring.MetalType
            };
        }

        public static Earring toEarringFromCreateDTO(this CreateEarringRequestDTO dto)
        {
            return new Earring
            {
                EarringId = Guid.NewGuid(),
                Name = dto.Name,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                ImageUrl = dto.ImageUrl,
                MetalType = dto.MetalType
            };
        }

        public static Earring toEarringFromUpdateDTO(this UpdateEarringRequestDTO dto)
        {
            return new Earring
            {
                EarringId = Guid.NewGuid(),
                Name = dto.Name,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                ImageUrl = dto.ImageUrl,
                MetalType = dto.MetalType
            };
        }
    }
}
