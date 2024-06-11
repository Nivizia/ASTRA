using DiamondAPI.Data;
using DiamondAPI.DTOs.Customer;
using DiamondAPI.DTOs.Diamondcertificate;
using DiamondAPI.Models;

namespace DiamondAPI.Interfaces
{
    public interface IDiamondcertificateRepository
    {
        Task<List<Diamondcertificate>> GetAllAsync();
        Task<Diamondcertificate?> GetByIDAsync(Guid guid);
        Task<Diamondcertificate> CreateAsync(Diamondcertificate diamondcertificate);
        Task<Diamondcertificate?> UpdateAsync(Guid guid, UpdateDiamondcertificateRequestDTO diamondcertificateDTO);
        Task<Diamondcertificate?> DeleteAsync(Guid guid);

    }
}
