using DiamondAPI.Data;
using DiamondAPI.DTOs.Customer;
using DiamondAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace DiamondAPI.Controllers
{
    [Route("DiamondAPI/Models/Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DiamondprojectContext _context;
        public CustomerController(DiamondprojectContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var customers = _context.Customers.ToList().Select(c => c.toCustomerDTO());
            return Ok(customers);
        }

        [HttpGet("{CustomerId}")]
        public IActionResult GetByID([FromRoute] Guid CustomerId)
        {
            var customer = _context.Customers.Find(CustomerId);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer.toCustomerDTO());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateCustomerRequestDTO customerDTO)
        {
            var customerModel = customerDTO.toCustomerFromCreateDTO();
            _context.Customers.Add(customerModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetByID), new { CustomerId = customerModel.CustomerId }, customerModel.toCustomerDTO());
        }

        [HttpPut]
        [Route("{CustomerId}")]
        public IActionResult Update([FromRoute] Guid CustomerId, [FromBody] UpdateCustomerRequestDTO customerDTO)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.CustomerId == CustomerId);
            if (customer == null)
            {
                return NotFound();
            }
            customer.FirstName = customerDTO.FirstName;
            customer.LastName = customerDTO.LastName;
            customer.Email = customerDTO.Email;
            customer.Password = customerDTO.Password;
            customer.PhoneNumber = customerDTO.PhoneNumber;
            customer.RegistrationDate = customerDTO.RegistrationDate;
            _context.SaveChanges();
            return Ok(customer.toCustomerDTO());
        }

        [HttpDelete]
        [Route("{CustomerId}")]
        public IActionResult Delete([FromRoute] Guid CustomerId)
        {
            var customer = _context.Customers.Find(CustomerId);
            if (customer == null)
            {
                return NotFound();
            }
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
