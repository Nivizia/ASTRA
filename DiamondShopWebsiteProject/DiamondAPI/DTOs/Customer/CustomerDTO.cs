namespace DiamondAPI.DTOs.Customer
{
    public class CustomerDTO
    {
        public Guid CustomerId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Username { get; set; }
    }
}
