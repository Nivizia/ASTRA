using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using DiamondAPI.Models;
using System.Collections.Generic;

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
            var firstName = customer.FirstName ?? throw new ArgumentNullException(nameof(customer.FirstName));
            var lastName = customer.LastName ?? throw new ArgumentNullException(nameof(customer.LastName));
            var password = customer.Password ?? throw new ArgumentNullException(nameof(customer.Password));
            var passwordLength = password.Length.ToString();
            var email = customer.Email ?? throw new ArgumentNullException(nameof(customer.Email));
            var phone = customer.PhoneNumber;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, customerId),
                new Claim("Username", customerUsername),
                new Claim("PasswordLength", passwordLength),
                new Claim("FirstName", firstName),
                new Claim("LastName", lastName),
                new Claim("Email", email),
            };

            if (!string.IsNullOrEmpty(phone))
            {
                claims.Add(new Claim("PhoneNumber", phone));
            }

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
