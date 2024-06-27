using DiamondAPI.DTOs.Diamond;
using DiamondAPI.Models;

namespace DiamondAPI.Interfaces
{
    public interface IDiamondRepository
    {
        Task<List<Diamond>> GetAllAsync();
        Task<Diamond?> GetByIDAsync(Guid? D_ProductID);
        Task<Diamond> CreateAsync(Diamond diamondModel);
        Task<Diamond?> UpdateAsync(Guid D_ProductID, ModelUpdateDiamondRequestDTO diamondDTO);
        Task<Diamond?> DeleteAsync(Guid D_ProductID);
        Task<List<Diamond>> FilterAsync(String DType, decimal? LowerPrice, decimal? UpperPrice, double? LowerCaratWeight, double? UpperCaratWeight, int? LowerColor, int? UpperColor, int? LowerClarity, int? UpperClariry, int? LowerCut, int? UpperCut);
    }
}
