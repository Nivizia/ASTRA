using DiamondAPI.DTOs.Orderitem;
using DiamondAPI.Models;

namespace DiamondAPI.Interfaces
{
    public interface IOrderitemRepository
    {
        Task<List<Orderitem>> GetAllAsync();
        Task<Orderitem?> GetByIDAsync(Guid OrderitemId);
        Task<Orderitem> CreateAsync(Orderitem orderitemModel);
        Task<Orderitem?> UpdateAsync(Guid OrderitemId, UpdateOrderitemRequestDTO orderitemDTO);
        Task<Orderitem?> DeleteAsync(Guid OrderitemId);
    }
}
