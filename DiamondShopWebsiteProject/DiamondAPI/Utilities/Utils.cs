using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace DiamondAPI.Utilities
{
    public static class Utils
    {
        public static string HmacSHA512(string key, string data)
        {
            using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key)))
            {
                var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public static string GetIpAddress(HttpContext context)
        {
            return context.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0";
        }
    }
}
