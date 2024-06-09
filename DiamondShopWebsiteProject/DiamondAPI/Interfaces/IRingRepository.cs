using DiamondAPI.DTOs.Ring;
using DiamondAPI.Models;

namespace DiamondAPI.Interfaces
{
    public interface IRingRepository
    {
        Task<List<Ring>> GetAllAsync();
        Task<Ring?> GetByIDAsync(Guid RingID);
        Task<Ring> CreateAsync(Ring ringModel);
        Task<Ring?> UpdateAsync(Guid RingID, UpdateRingRequestDTO ringDTO);
        Task<Ring?> DeleteAsync(Guid RingID);
    }
}
