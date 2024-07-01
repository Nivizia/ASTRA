using DiamondAPI.DTOs.EarringPairing;
using DiamondAPI.Models;

namespace DiamondAPI.Interfaces
{
    public interface IEarringPairingRepository
    {
        Task<List<Earringpairing>> GetAllAsync();
        Task<Earringpairing?> GetByIDAsync(Guid EProductID);
        Task<Earringpairing> CreateAsync(Earringpairing earringpairing);
        Task<Earringpairing?> UpdateAsync(UpdateEarringPairingRequestDTO update);
        Task<Earringpairing?> DeleteAsync(Guid EProductID);
        Task<List<Earringpairing>> CreateEarringPairings(List<Earringpairing> earringPairings);
    }
}
