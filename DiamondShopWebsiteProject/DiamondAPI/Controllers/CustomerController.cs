using DiamondAPI.Data;
using DiamondAPI.DTOs.Customer;
using DiamondAPI.Interfaces;
using DiamondAPI.Mappers;
using DiamondAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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


        //List out all customers
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerRepo.GetAllAsync();
            var customerDTOs = customers.Select(c => c.toCustomerDTO());
            return Ok(customerDTOs);
        }

        //Get a customer by ID
        [ResponseCache(Duration = 60)]
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

        //Create a customer
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

        //Update a customer by ID
        [HttpPut]
        [Route("{CustomerId}")]
        public async Task<IActionResult> Update([FromRoute] Guid CustomerId, [FromBody] UpdateCustomerRequestDTO customerDTO)
        {
            var customer = await _customerRepo.UpdateAsync(CustomerId, customerDTO);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer.toCustomerDTO());
        }

        //Delete a customer by ID
        [HttpDelete]
        [Route("{CustomerId}")]
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
