using System.ComponentModel.DataAnnotations;

namespace DiamondAPI.DTOs.Customer
{
    public class LoginRequestDTO
    {
        [Required]
        [StringLength(50, ErrorMessage = "Username can't be longer than 50 characters.")]
        public string? Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Password can't be longer than 100 characters.")]
        public string? Password { get; set; }
    }
}
