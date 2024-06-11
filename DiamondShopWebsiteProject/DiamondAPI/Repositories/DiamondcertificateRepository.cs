using DiamondAPI.Data;
using DiamondAPI.DTOs.Diamondcertificate;
using DiamondAPI.Interfaces;
using DiamondAPI.Models;

namespace DiamondAPI.Repositories
{
    public class DiamondcertificateRepository : IDiamondcertificateRepository
    {
        private readonly DiamondprojectContext _context;
        public DiamondcertificateRepository(DiamondprojectContext context)
        {
            _context = context;
        }

        public Task<Diamondcertificate> CreateAsync(Diamondcertificate diamondcertificate)
        {
            throw new NotImplementedException();
        }

        public Task<Diamondcertificate?> DeleteAsync(Guid guid)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Diamondcertificate>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Diamondcertificate?> GetByIDAsync(Guid guid)
        {
            throw new NotImplementedException();
        }

        public Task<Diamondcertificate?> UpdateAsync(Guid guid, UpdateDiamondcertificateRequestDTO diamondcertificateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
