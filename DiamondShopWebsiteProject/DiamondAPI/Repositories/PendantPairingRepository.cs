using DiamondAPI.Data;
using DiamondAPI.Interfaces;
using DiamondAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DiamondAPI.Repositories
{
    public class PendantPairingRepository : IPendantPairingRepository
    {
        private readonly DiamondprojectContext _context;

        public PendantPairingRepository(DiamondprojectContext context)
        {
            _context = context;
        }

        public async Task<Pendantpairing> AddPendantPairingAsync(Pendantpairing pendantPairing)
        {
            await _context.Pendantpairings.AddAsync(pendantPairing);
            await _context.SaveChangesAsync();
            return pendantPairing;
        }

        public async Task<List<Pendantpairing>> CreatePendantPairings(List<Pendantpairing> pendantPairings)
        {
            await _context.Pendantpairings.AddRangeAsync(pendantPairings);
            await _context.SaveChangesAsync();
            return pendantPairings;
        }

        public async Task<Pendantpairing?> DeletePendantPairingAsync(Guid id)
        {
            var pendantPairing = await _context.Pendantpairings.FindAsync(id);
            if (pendantPairing == null)
                return null;

            _context.Pendantpairings.Remove(pendantPairing);
            await _context.SaveChangesAsync();
            return pendantPairing;
        }

        public async Task<Pendantpairing?> GetPendantPairingAsync(Guid id)
        {
            return await _context.Pendantpairings.FindAsync(id);
        }

        public async Task<List<Pendantpairing>> GetPendantPairingsAsync()
        {
            return await _context.Pendantpairings.ToListAsync();
        }

        public Task<Pendantpairing?> UpdatePendantPairingAsync(Pendantpairing pendantPairing)
        {
            throw new NotImplementedException();
        }
    }
}
