using DiamondAPI.DTOs.Pendant;
using DiamondAPI.Models;

namespace DiamondAPI.Mappers
{
    public static class PendantMappers
    {
        public static PendantDTO ToPendantDTO(this Pendant pendant)
        {
            return new PendantDTO
            {
                PendantId = pendant.PendantId,
                Name = pendant.Name,
                Price = pendant.Price,
                StockQuantity = pendant.StockQuantity,
                ImageUrl = pendant.ImageUrl,
                MetalType = pendant.MetalType
            };
        }

        public static Pendant toPendantFromCreateDTO(this CreatePendantRequestDTO createPendantRequestDTO)
        {
            return new Pendant
            {
                PendantId = Guid.NewGuid(),
                Name = createPendantRequestDTO.Name,
                Price = createPendantRequestDTO.Price,
                StockQuantity = createPendantRequestDTO.StockQuantity,
                ImageUrl = createPendantRequestDTO.ImageUrl,
                MetalType = createPendantRequestDTO.MetalType
            };
        }
    }
}
