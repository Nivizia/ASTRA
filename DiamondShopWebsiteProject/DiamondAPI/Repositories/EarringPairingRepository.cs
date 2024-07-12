using DiamondAPI.Data;
using DiamondAPI.DTOs.EarringPairing;
using DiamondAPI.Interfaces;
using DiamondAPI.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Earringpairing>> CreateEarringPairings(List<Earringpairing> earringPairings)
        {
            await _context.Earringpairings.AddRangeAsync(earringPairings);
            await _context.SaveChangesAsync();
            return earringPairings;
        }

        public async Task<Earringpairing?> DeleteAsync(Guid? EProductID)
        {
            var earringpairing = await _context.Earringpairings.FindAsync(EProductID);
            if (earringpairing == null)
                return null;

            _context.Earringpairings.Remove(earringpairing);
            await _context.SaveChangesAsync();
            return earringpairing;
        }

        public async Task<List<Earringpairing>> GetAllAsync()
        {
            return await _context.Earringpairings.ToListAsync();
        }

        public async Task<Earringpairing?> GetByIDAsync(Guid? EProductID)
        {
            if (EProductID == null)
                return null;
            return await _context.Earringpairings.FindAsync(EProductID);
        }

        public Task<Earringpairing?> UpdateAsync(UpdateEarringPairingRequestDTO update)
        {
            throw new NotImplementedException();
        }
    }
}
