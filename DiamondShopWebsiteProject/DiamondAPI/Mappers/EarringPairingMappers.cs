using DiamondAPI.DTOs.EarringPairing;
using DiamondAPI.Models;

namespace DiamondAPI.Mappers
{
    public static class EarringPairingMappers
    {
        public static EarringPairingDTO toEarringPairingDTO(this Earringpairing earringpairing)
        {
            return new EarringPairingDTO
            {
                EProductId = earringpairing.EProductId,
                EarringId = earringpairing.EarringId,
                DiamondId = earringpairing.DiamondId
            };
        }

        public static Earringpairing toEarringPairingFromCreateDTO(this CreateEarringPairingRequestDTO request)
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
