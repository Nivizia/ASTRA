using System.ComponentModel.DataAnnotations;

namespace DiamondAPI.DTOs.Customer
{
    public class RegisterCustomerDTO
    {
        [Required]
        [StringLength(32, MinimumLength = 1, ErrorMessage = "Username cannot be blank or longer than 32 characters.")]
        public string? Username { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 32 characters.")]
        public string? Password { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 1, ErrorMessage = "Firstname cannot be blank or longer than 32 characters.")]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 1, ErrorMessage = "Lastname cannot be blank or longer than 32 characters.")]
        public string? LastName { get; set; }
    }
}
