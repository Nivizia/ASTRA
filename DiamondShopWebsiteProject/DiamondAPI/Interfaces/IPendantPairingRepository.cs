using DiamondAPI.Models;

namespace DiamondAPI.Interfaces
{
    public interface IPendantPairingRepository
    {
        Task<Pendantpairing?> GetPendantPairingAsync(Guid? id);
        Task<List<Pendantpairing>> GetPendantPairingsAsync();
        Task<Pendantpairing> AddPendantPairingAsync(Pendantpairing pendantPairing);
        Task<Pendantpairing?> UpdatePendantPairingAsync(Pendantpairing pendantPairing);
        Task<Pendantpairing?> DeletePendantPairingAsync(Guid? id);
        Task<List<Pendantpairing>> CreatePendantPairings(List<Pendantpairing> pendantPairings);
    }
}
