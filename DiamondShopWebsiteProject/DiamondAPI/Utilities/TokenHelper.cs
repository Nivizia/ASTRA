using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class TokenHelper
{
    private static readonly string EncryptionKey = "InaIsTheBestVTuberFrFrFr"; // Ensure this key length is appropriate

    public static string GenerateToken(Guid orderId, DateTime? expiration = null)
    {
        var token = expiration.HasValue ? $"{orderId}|{expiration:o}" : $"{orderId}|no-expiration";
        return Base64UrlEncode(Encrypt(token));
    }

    public static (Guid orderId, DateTime? expiration)? ValidateToken(string token)
    {
        var decodedToken = Base64UrlDecode(token);
        var decryptedToken = Decrypt(decodedToken);
        if (decryptedToken == null)
        {
            Console.WriteLine("1");
            return null;
        }

        var parts = decryptedToken.Split('|');
        if (parts.Length != 2 || !Guid.TryParse(parts[0], out var orderId))
        {
            Console.WriteLine("2");
            return null;
        }

        if (parts[1] == "no-expiration")
        {
            Console.WriteLine("3");
            return (orderId, null);
        }

        if (!DateTime.TryParse(parts[1], out var expiration))
        {
            Console.WriteLine("4");
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
        catch (FormatException ex)
        {
            Console.WriteLine("Format exception: " + ex.Message);
            return null;
        }
        catch (CryptographicException ex)
        {
            Console.WriteLine("Cryptographic exception: " + ex.Message);
            return null;
        }
    }

    private static string Base64UrlEncode(string input)
    {
        var output = Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        return output.Replace('+', '-').Replace('/', '_').Replace("=", string.Empty);
    }

    private static string Base64UrlDecode(string input)
    {
        var output = input.Replace('-', '+').Replace('_', '/');
        switch (output.Length % 4)
        {
            case 2: output += "=="; break;
            case 3: output += "="; break;
        }
        var byteArray = Convert.FromBase64String(output);
        return Encoding.UTF8.GetString(byteArray);
    }
}
