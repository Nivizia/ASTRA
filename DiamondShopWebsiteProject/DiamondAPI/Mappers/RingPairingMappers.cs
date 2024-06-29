using DiamondAPI.DTOs.RingPairing;
using DiamondAPI.Models;

namespace DiamondAPI.Mappers
{
    public static class RingPairingMappers
    {
        public static RingPairingDTO ToRingPairingDTO(this Ringpairing ringPairing)
        {
            return new RingPairingDTO
            {
                RProductId = ringPairing.RProductId,
                RingId = ringPairing.RingId,
                DiamondId = ringPairing.DiamondId
            };
        }
        public static Ringpairing ToRingPairingFromCreateDTO(this CreateRingPairingRequestDTO createRingPairingRequestDTO)
        {
            return new Ringpairing
            {
                RProductId = Guid.NewGuid(),
                RingId = createRingPairingRequestDTO.RingId,
                DiamondId = createRingPairingRequestDTO.DiamondId
            };
        }
    }
}
