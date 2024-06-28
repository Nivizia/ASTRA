using DiamondAPI.DTOs.EarringPairing;
using DiamondAPI.Models;

namespace DiamondAPI.Mappers
{
    public static class EarringPairingMappers
    {
        public static EarringPairingDTO ToEarringPairingDTO(this Earringpairing earringpairing)
        {
            return new EarringPairingDTO
            {
                EProductId = earringpairing.EProductId,
                EarringId = earringpairing.EarringId,
                DiamondId = earringpairing.DiamondId
            };
        }

        public static Earringpairing ToEarringPairingFromCreateDTO(this CreateEarringPairingRequestDTO request)
        {
            return new Earringpairing
            {
                EProductId = Guid.NewGuid(),
                EarringId = request.EarringId,
                DiamondId = request.DiamondId
            };
        }
    }
}
