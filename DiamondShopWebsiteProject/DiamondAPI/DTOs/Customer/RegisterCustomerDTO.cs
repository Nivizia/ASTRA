using System.ComponentModel.DataAnnotations;

namespace DiamondAPI.DTOs.Customer
{
    public class RegisterCustomerDTO
    {
        [Required]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "Username can't be longer than 16 and shorter than 4 characters.")]
        public string? Username { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 8, ErrorMessage = "Password can't be longer than 16 and shorter than 4 characters.")]
        public string? Password { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "Firstname can't be longer than 16 and shorter than 4 characters.")]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "Lastname can't be longer than 16 and shorter than 4 characters.")]
        public string? LastName { get; set; }
    }
}
