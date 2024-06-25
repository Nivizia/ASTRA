using DiamondAPI.Data;
using DiamondAPI.Interfaces;
using DiamondAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DiamondAPI.Repositories
{
    public class RingPairingRepository : IRingPairingRepository
    {
        private readonly DiamondprojectContext _context;

        public RingPairingRepository(DiamondprojectContext context)
        {
            _context = context;
        }
        public async Task<Ringpairing> CreateAsync(Ringpairing ringPairing)
        {
            await _context.Ringpairings.AddAsync(ringPairing);
            await _context.SaveChangesAsync();
            return ringPairing;
        }

        public async Task<Ringpairing?> Delete(Guid id)
        {
            var ringPairing = await _context.Ringpairings.FirstOrDefaultAsync(r => r.RProductId.Equals(id));

            if (ringPairing == null)
                return null;

            _context.Ringpairings.Remove(ringPairing);

            await _context.SaveChangesAsync();
            return ringPairing;
        }

        public async Task<List<Ringpairing>> GetAll()
        {
            return await _context.Ringpairings.ToListAsync();
        }

        public async Task<Ringpairing?> GetById(Guid id)
        {
            return await _context.Ringpairings.FindAsync(id);
        }

        public async Task<Ringpairing?> Update(Ringpairing ringPairing)
        {
            var existingRingPairing = await _context.Ringpairings.FirstOrDefaultAsync(r => r.RProductId.Equals(ringPairing.RProductId));

            if (existingRingPairing == null)
                return null;

            existingRingPairing.RingId = ringPairing.RingId;
            existingRingPairing.DiamondId = ringPairing.DiamondId;

            await _context.SaveChangesAsync();

            return existingRingPairing;
        }
    }
}
