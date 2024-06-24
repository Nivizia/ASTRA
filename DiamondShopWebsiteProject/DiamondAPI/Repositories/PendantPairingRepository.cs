using DiamondAPI.Data;
using DiamondAPI.Interfaces;
using DiamondAPI.Models;

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

        public Task<Pendantpairing> DeletePendantPairingAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Pendantpairing> GetPendantPairingAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Pendantpairing>> GetPendantPairingsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Pendantpairing> UpdatePendantPairingAsync(Pendantpairing pendantPairing)
        {
            throw new NotImplementedException();
        }
    }
}
