using Application.Interfaces.HashManagement;

namespace Infrastructure.Hashing
{
    //implemented class for Hash management using BCrypt Paackage
    public class HashManagerService : IHashManager
    {
        public string HashPassword(string password)   //method to Hash User password
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }

        public (string plain, string hashed) GenerateHashedToken()
        {
            //url safe string for plain Token
            var plain = Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");

            var hashed = BCrypt.Net.BCrypt.HashPassword(plain, workFactor: 12);

            return (plain, hashed);
        }

        public bool VerifyHashedValue(string PlainText, string HashedValue)
        {
            return BCrypt.Net.BCrypt.Verify(PlainText, HashedValue);
        }
    }
}
