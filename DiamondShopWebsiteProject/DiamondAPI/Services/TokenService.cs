using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using DiamondAPI.Models;

namespace DiamondAPI.Services
{
    public class TokenService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expiresInMinutes;

        public TokenService(IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");

            // Check and assign default values if the configuration is missing
            _secretKey = jwtSettings["Secret"] ?? throw new ArgumentNullException("Secret key is missing in configuration");
            _issuer = jwtSettings["Issuer"] ?? throw new ArgumentNullException("Issuer is missing in configuration");
            _audience = jwtSettings["Audience"] ?? throw new ArgumentNullException("Audience is missing in configuration");

            // Parse the expiration minutes with a default value
            if (!int.TryParse(jwtSettings["ExpiresInMinutes"], out _expiresInMinutes))
            {
                _expiresInMinutes = 120; // Default value
            }
        }

        public string GenerateToken(Customer customer)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Check customer properties for null values
            var customerId = customer.CustomerId.ToString() ?? throw new ArgumentNullException(nameof(customer.CustomerId));
            var customerUsername = customer.Username ?? throw new ArgumentNullException(nameof(customer.Username));
            var fullName = $"{customer.FirstName ?? string.Empty} {customer.LastName ?? string.Empty}".Trim();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, customerId),
                new Claim(JwtRegisteredClaimNames.UniqueName, customerUsername),
                new Claim("FullName", fullName)
            };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_expiresInMinutes),
                signingCredentials: credentials);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            // Log the generated token
            Console.WriteLine("Generated JWT Token: " + jwtToken);

            return jwtToken;
        }
    }
}
