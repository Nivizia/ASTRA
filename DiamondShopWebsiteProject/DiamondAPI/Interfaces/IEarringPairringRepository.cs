using DiamondAPI.DTOs.EarringPairing;
using DiamondAPI.Models;

namespace DiamondAPI.Interfaces
{
    public interface IEarringPairringRepository
    {
        Task<List<Earringpairing>> GetAllAsync();
        Task<Earringpairing?> GetByIDAsync(Guid EProductID);
        Task<Earringpairing> CreateAsync(CreateEarringPairingRequestDTO request);
        Task<Earringpairing> UpdateAsync(UpdateEarringPairingRequestDTO update);
        Task<Earringpairing> DeleteAsync(Guid EProductID);
    }
}
