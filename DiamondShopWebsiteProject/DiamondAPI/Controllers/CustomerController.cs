using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using DiamondAPI.DTOs.Customer;
using DiamondAPI.Interfaces;
using DiamondAPI.Mappers;
using DiamondAPI.Models;
using DiamondAPI.Services;
using DiamondAPI.Data;

namespace DiamondAPI.Controllers
{
    [Route("DiamondAPI/Models/Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DiamondprojectContext _context;
        private readonly ICustomerRepository _customerRepo;
        private readonly TokenService _tokenService;

        public CustomerController(DiamondprojectContext context, ICustomerRepository customerRepo, TokenService tokenService)
        {
            _context = context;
            _customerRepo = customerRepo;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _customerRepo.LoginAsync(loginRequest.Username, loginRequest.Password);
            if (customer == null)
                return Unauthorized();

            var token = _tokenService.GenerateToken(customer);
            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCustomerDTO registerRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _customerRepo.UserExistsAsync(registerRequest.Username))
                return Conflict(new { message = "Username already exists" });

            var customer = registerRequest.toCustomerFromRegisterDTO();
            await _customerRepo.RegisterAsync(customer);

            return StatusCode(201);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerRepo.GetAllAsync();
            var customerDTOs = customers.Select(c => c.toCustomerDTO());
            return Ok(customerDTOs);
        }

        [HttpGet("{CustomerId}")]
        public async Task<IActionResult> GetByID([FromRoute] Guid CustomerId)
        {
            var customer = await _customerRepo.GetByIDAsync(CustomerId);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer.toCustomerDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerRequestDTO customerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customerModel = customerDTO.toCustomerFromCreateDTO();
            await _customerRepo.CreateAsync(customerModel);
            return CreatedAtAction(nameof(GetByID), new { CustomerId = customerModel.CustomerId }, customerModel.toCustomerDTO());
        }

        [HttpPut]
        [Route("{CustomerId}")]
        public async Task<IActionResult> Update([FromRoute] Guid CustomerId, [FromBody] UpdateCustomerRequestDTO customerDTO)
        {
            var customer = await _customerRepo.GetByIDAsync(CustomerId);
            if (customer == null)
            {
                return NotFound();
            }

            // Update fields only if they are provided
            if (!string.IsNullOrEmpty(customerDTO.FirstName)) customer.FirstName = customerDTO.FirstName;
            if (!string.IsNullOrEmpty(customerDTO.LastName)) customer.LastName = customerDTO.LastName;
            if (!string.IsNullOrEmpty(customerDTO.Email)) customer.Email = customerDTO.Email;
            if (!string.IsNullOrEmpty(customerDTO.Username)) customer.Username = customerDTO.Username;
            if (!string.IsNullOrEmpty(customerDTO.PhoneNumber)) customer.PhoneNumber = customerDTO.PhoneNumber;

            var updatedCustomer = await _customerRepo.UpdateAsync(CustomerId, customerDTO);

            // Generate a new token
            var newToken = _tokenService.GenerateToken(updatedCustomer);

            return Ok(new { customer = customer.toCustomerDTO(), token = newToken });
        }

        [HttpDelete("{CustomerId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid CustomerId)
        {
            var customer = await _customerRepo.DeleteAsync(CustomerId);
            if (customer == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
