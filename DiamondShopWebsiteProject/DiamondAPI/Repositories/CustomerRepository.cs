using DiamondAPI.Data;
using DiamondAPI.DTOs.Customer;
using DiamondAPI.Interfaces;
using DiamondAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DiamondAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DiamondprojectContext _context;
        public CustomerRepository(DiamondprojectContext context)
        {
            _context = context;
        }

        public async Task<Customer> CreateAsync(Customer customerModel)
        {
            await _context.Customers.AddAsync(customerModel);
            await _context.SaveChangesAsync();
            return customerModel;
        }

        public async Task<Customer?> DeleteAsync(Guid CustomerId)
        {
            var customerModel = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == CustomerId);
            if (customerModel == null)
            {
                return null;
            }
            _context.Customers.Remove(customerModel);
            await _context.SaveChangesAsync();
            return customerModel;
        }

        public Task<List<Customer>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Customer?> GetByIDAsync(Guid CustomerId)
        {
            throw new NotImplementedException();
        }

        public Task<Customer?> UpdateAsync(Guid CustomerId, UpdateCustomerRequestDTO customerDTO)
        {
            throw new NotImplementedException();
        }
    }
}
