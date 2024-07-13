using System.ComponentModel.DataAnnotations;

namespace DiamondAPI.DTOs.Customer
{
    public class LoginRequestDTO
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
