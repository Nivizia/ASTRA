using DiamondAPI.Data;
using DiamondAPI.DTOs.EarringPairing;
using DiamondAPI.Interfaces;
using DiamondAPI.Models;

namespace DiamondAPI.Repositories
{
    public class EarringPairingRepository : IEarringPairingRepository
    {
        private readonly DiamondprojectContext _context;

        public EarringPairingRepository(DiamondprojectContext context)
        {
            _context = context;
        }

        public async Task<Earringpairing> CreateAsync(Earringpairing earringpairing)
        {
            await _context.Earringpairings.AddAsync(earringpairing);
            await _context.SaveChangesAsync();
            return earringpairing;
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
