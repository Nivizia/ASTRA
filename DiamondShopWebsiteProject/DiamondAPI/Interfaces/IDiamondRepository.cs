using DiamondAPI.DTOs.Diamond;
using DiamondAPI.Models;

namespace DiamondAPI.Interfaces
{
    public interface IDiamondRepository
    {
        Task<List<Diamond>> GetAllAsync();
        Task<Diamond?> GetByIDAsync(Guid D_ProductID);
        Task<Diamond> CreateAsync(Diamond diamondModel);
        Task<Diamond?> UpdateAsync(Guid D_ProductID, UpdateDiamondRequestDTO diamondDTO);
        Task<Diamond?> DeleteAsync(Guid D_ProductID);
    }
}
