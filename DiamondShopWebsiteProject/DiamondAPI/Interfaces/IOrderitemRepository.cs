using DiamondAPI.DTOs.Orderitem;
using DiamondAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DiamondAPI.Interfaces
{
    public interface IOrderitemRepository
    {
        Task<List<Orderitem>> GetAllAsync();
        Task<Orderitem?> GetByIDAsync(Guid OrderitemId);
        Task<Orderitem> CreateAsync(Orderitem orderitemModel);
        Task<Orderitem?> UpdateAsync(Guid OrderitemId, UpdateOrderitemRequestDTO orderitemDTO);
        Task<Orderitem?> DeleteAsync(Guid OrderitemId);
        Task<List<Orderitem>> CreateOrderItems(List<Orderitem> orderitems);
    }
}
