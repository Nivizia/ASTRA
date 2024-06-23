using DiamondAPI.Models;

namespace DiamondAPI.Interfaces
{
    public interface IRingPairingRepository
    {
        public Task<List<Ringpairing>> GetAll();
        public Task<Ringpairing?> GetById(Guid id);
        public Task<Ringpairing> Create(Ringpairing ringPairing);
        public Task<Ringpairing?> Update(Ringpairing ringPairing);
        public Task<Ringpairing?> Delete(Guid id);
    }
}
