namespace Application.Interfaces.HashManagement
{
    public interface IHashManager   //interface for Hash management
    {
        string BCryptHashPassword(string password);   //method to Hash user Password
        (string plain, string hashed) BCryptGenerateHashedToken();   //method to generate hashed Guid as Token
        bool BCryptVerifyHashedValue(string PlainText, string HashedValue);   //method to verify a PlainText with Hashed value
        (string plain, string hashed) HMACSHA256GenerateHashedToken(string key);   //method to generate token with secretKey and always same output
        string HMACSHA256HashValue(string key, string plain); //method to hash given value with secretKey and always same output
    }
}
