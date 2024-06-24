using DiamondAPI.DTOs.PendantPairing;
using DiamondAPI.Models;

namespace DiamondAPI.Mappers
{
    public static class PendantPairingMappers
    {
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
