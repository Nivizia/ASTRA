using DiamondAPI.DTOs.Customer;
using DiamondAPI.Models;

namespace DiamondAPI.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer?> GetByIDAsync(Guid CustomerId);
        Task<Customer> CreateAsync(Customer customerModel);
        Task<Customer?> UpdateAsync(Guid CustomerId, UpdateCustomerRequestDTO customerDTO);
        Task<Customer?> DeleteAsync(Guid CustomerId);
    }
}
