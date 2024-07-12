using DiamondAPI.Models;

namespace DiamondAPI.Interfaces
{
    public interface IRingPairingRepository
    {
        public Task<List<Ringpairing>> GetAll();
        public Task<Ringpairing?> GetById(Guid? id);
        public Task<Ringpairing> CreateAsync(Ringpairing ringPairing);
        public Task<Ringpairing?> Update(Ringpairing ringPairing);
        public Task<Ringpairing?> Delete(Guid? id);
        public Task<List<Ringpairing>> CreateRingPairings(List<Ringpairing> ringpairings);
    }
}
