using DiamondAPI.DTOs.PendantPairing;
using DiamondAPI.Models;

namespace DiamondAPI.Mappers
{
    public static class PendantPairingMappers
    {
        public static PendantPairingDTO ToPendantPairingDTO(this Pendantpairing pendantPairing)
        {
            return new PendantPairingDTO
            {
                PProductId = pendantPairing.PProductId,
                PendantId = pendantPairing.PendantId,
                DiamondId = pendantPairing.DiamondId
            };
        }

        public static Pendantpairing ToPendantPairingFromCreateDTO(this CreatePendantPairingRequestDTO createPendantPairingRequestDTO)
        {
            return new Pendantpairing
            {
                PProductId = Guid.NewGuid(),
                PendantId = createPendantPairingRequestDTO.PendantId,
                DiamondId = createPendantPairingRequestDTO.DiamondId
            };
        }
    }
}
