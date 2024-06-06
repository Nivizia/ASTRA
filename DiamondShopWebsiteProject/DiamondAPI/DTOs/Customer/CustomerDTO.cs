namespace DiamondAPI.DTOs.Customer
{
    public class CustomerDTO
    {
        public Guid CustomerId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime? RegistrationDate { get; set; }
    }
}
