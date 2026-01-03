using Application.Interfaces.HashManagement;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Hashing
{
    //implemented class for Hash management using BCrypt Paackage
    public class HashManagerService : IHashManager
    {
        public string BCryptHashPassword(string password)   //method to Hash User password
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }

        public (string plain, string hashed) BCryptGenerateHashedToken()
        {
            //url safe string for plain Token
            var plain = Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");

            var hashed = BCrypt.Net.BCrypt.HashPassword(plain, workFactor: 12);

            return (plain, hashed);
        }

        public bool BCryptVerifyHashedValue(string PlainText, string HashedValue)
        {
            return BCrypt.Net.BCrypt.Verify(PlainText, HashedValue);
        }

        public (string plain, string hashed) HMACSHA256GenerateHashedToken(string key)
        {
            //generate plain token
            var plain = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            var byteKey = Convert.FromBase64String(key);   //convert to byte[] key
            using var hmac = new HMACSHA256(byteKey);   //using HmacSha256 algorithm
            var hashed = hmac.ComputeHash(Encoding.UTF8.GetBytes(plain));

            return (plain, Convert.ToBase64String(hashed));
        }

        public string HMACSHA256HashValue(string key, string plain)
        {
            var byteKey = Convert.FromBase64String(key);   //convert to byte[] key
            using var hmac = new HMACSHA256(byteKey);   //using HmacSha256 algorithm
            var hashed = hmac.ComputeHash(Encoding.UTF8.GetBytes(plain));

            return Convert.ToBase64String(hashed);
        }
    }
}
