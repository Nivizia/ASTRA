using DiamondAPI.DTOs.EarringPairing;
using DiamondAPI.Interfaces;
using DiamondAPI.Models;

namespace DiamondAPI.Repositories
{
    public class EarringPairingRepository : IEarringPairingRepository
    {
        public Task<Earringpairing> CreateAsync(CreateEarringPairingRequestDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<Earringpairing> DeleteAsync(Guid EProductID)
        {
            throw new NotImplementedException();
        }

        public Task<List<Earringpairing>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Earringpairing?> GetByIDAsync(Guid EProductID)
        {
            throw new NotImplementedException();
        }

        public Task<Earringpairing> UpdateAsync(UpdateEarringPairingRequestDTO update)
        {
            throw new NotImplementedException();
        }
    }
}
