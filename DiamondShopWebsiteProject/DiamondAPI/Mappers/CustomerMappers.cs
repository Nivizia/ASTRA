using DiamondAPI.DTOs.Customer;
using DiamondAPI.Models;

namespace DiamondAPI.Mappers
{
    public static class CustomerMappers
    {
        public static CustomerDTO toCustomerDTO(this Customer customer)
        {
            return new CustomerDTO
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Username = customer.Username
            };
        }

        public static Customer toCustomerFromCreateDTO(this CreateCustomerRequestDTO customer)
        {
            return new Customer
            {
                CustomerId = Guid.NewGuid(),
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Username = customer.Username,
                Password = customer.Password,
                PhoneNumber = customer.PhoneNumber,
                RegistrationDate = customer.RegistrationDate
            };
        }

        public static Customer toCustomerFromRegisterDTO(this RegisterCustomerDTO customer)
        {
            return new Customer
            {
                CustomerId = Guid.NewGuid(),
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Username = customer.Username,
                Password = customer.Password,
                Email = customer.Email,
            };
        }
    }
}
