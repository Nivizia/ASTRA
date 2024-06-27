using DiamondAPI.DTOs.Pendant;
using DiamondAPI.Models;

namespace DiamondAPI.Interfaces
{
    public interface IPendantRepository
    {
        Task<List<Pendant>> GetAllAsync();
        Task<Pendant?> GetByIDAsync(Guid? PendantID);
        Task<Pendant> CreateAsync(Pendant pendantModel);
        Task<Pendant?> UpdateAsync(Guid PendantID, UpdatePendantRequestDTO pendantDTO);
        Task<Pendant?> DeleteAsync(Guid PendantID);
    }
}
