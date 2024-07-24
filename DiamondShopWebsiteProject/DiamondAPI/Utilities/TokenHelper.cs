using System;
using System.IO;
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
            Console.WriteLine("1");
            Console.ReadLine();
            return null;
        }

        var parts = decryptedToken.Split('|');
        if (parts.Length != 2 || !Guid.TryParse(parts[0], out var orderId))
        {
            Console.WriteLine("2");
            Console.ReadLine();
            return null;
        }

        if (parts[1] == "no-expiration")
        {
            Console.WriteLine("3");
            Console.ReadLine();
            return (orderId, null);
        }

        if (!DateTime.TryParse(parts[1], out var expiration))
        {
            Console.WriteLine("4");
            Console.ReadLine();
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
            aes.GenerateIV();
            var iv = aes.IV;
            var encryptor = aes.CreateEncryptor(aes.Key, iv);
            using (var ms = new MemoryStream())
            {
                ms.Write(iv, 0, iv.Length);
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
        try
        {
            var key = Encoding.UTF8.GetBytes(EncryptionKey);
            var buffer = Convert.FromBase64String(cipherText);
            using (var ms = new MemoryStream(buffer))
            {
                var iv = new byte[16];
                ms.Read(iv, 0, iv.Length);
                using (var aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (var sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
        catch (FormatException)
        {
            // Log the error or handle it as needed
            Console.WriteLine("Format exception");
            return null;
        }
        catch (CryptographicException)
        {
            Console.WriteLine("Cryptographic exception");
            // Log the error or handle it as needed
            return null;
        }
    }
}
