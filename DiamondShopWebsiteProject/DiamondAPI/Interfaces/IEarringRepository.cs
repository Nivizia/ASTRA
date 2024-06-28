using DiamondAPI.DTOs.Earring;
using DiamondAPI.Models;

namespace DiamondAPI.Interfaces
{
    public interface IEarringRepository
    {
        Task<List<Earring>> GetAllAsync();
        Task<Earring?> GetByIDAsync(Guid? EarringID);
        Task<Earring> CreateAsync(Earring earringModel);
        Task<Earring?> UpdateAsync(Guid EarringID, UpdateEarringRequestDTO earringDTO);
        Task<Earring?> DeleteAsync(Guid EarringID);
    }
}
