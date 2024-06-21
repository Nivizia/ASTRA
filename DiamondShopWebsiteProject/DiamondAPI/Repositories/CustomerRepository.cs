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

        public async Task<Customer?> LoginAsync(string username, string password)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Username == username && c.Password == password);

            if (customer == null)
                return null;

            return customer;
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
                return null;

            _context.Customers.Remove(customerModel);
            await _context.SaveChangesAsync();
            return customerModel;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetByIDAsync(Guid CustomerId)
        {
            return await _context.Customers.FindAsync(CustomerId);
        }

        public async Task<Customer?> UpdateAsync(Guid CustomerId, UpdateCustomerRequestDTO customerDTO)
        {
            var existingCustomer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == CustomerId);

            if (existingCustomer == null)
            {
                return null;
            }

            existingCustomer.FirstName = customerDTO.FirstName;
            existingCustomer.LastName = customerDTO.LastName;
            existingCustomer.Email = customerDTO.Email;
            existingCustomer.Username = customerDTO.Username;
            existingCustomer.Password = customerDTO.Password;
            existingCustomer.PhoneNumber = customerDTO.PhoneNumber;
            existingCustomer.RegistrationDate = customerDTO.RegistrationDate;

            await _context.SaveChangesAsync();

            return existingCustomer;
        }

        public async Task<Customer> RegisterAsync(Customer customerModel)
        {
            await _context.Customers.AddAsync(customerModel);
            await _context.SaveChangesAsync();

            return customerModel;
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _context.Customers.AnyAsync(u => u.Username == username);
        }
    }
}
