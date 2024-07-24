using System;
using System.Security.Cryptography;
using System.Text;

public static class TokenHelper
{
    private static readonly string EncryptionKey = "InaIsTheBestVTuberFrFrFr"; // Change this to a secure key

    public static string GenerateToken(Guid orderId, DateTime? expiration = null)
    {
        var token = expiration.HasValue ? $"{orderId}|{expiration:o}" : $"{orderId}|no-expiration";
        return Encrypt(token);
    }

    public static (Guid orderId, DateTime? expiration)? ValidateToken(string token)
    {
        var decryptedToken = Decrypt(token);
        if (decryptedToken == null)
        {
            return null;
        }

        var parts = decryptedToken.Split('|');
        if (parts.Length != 2 || !Guid.TryParse(parts[0], out var orderId))
        {
            return null;
        }

        if (parts[1] == "no-expiration")
        {
            return (orderId, null);
        }

        if (!DateTime.TryParse(parts[1], out var expiration))
        {
            return null;
        }

        return (orderId, expiration);
    }

    private static string Encrypt(string plainText)
    {
        var key = Encoding.UTF8.GetBytes(EncryptionKey);
        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = new byte[16];
            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (var ms = new System.IO.MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    private static string Decrypt(string cipherText)
    {
        var key = Encoding.UTF8.GetBytes(EncryptionKey);
        var buffer = Convert.FromBase64String(cipherText);
        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = new byte[16];
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (var ms = new System.IO.MemoryStream(buffer))
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var sr = new StreamReader(cs))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
