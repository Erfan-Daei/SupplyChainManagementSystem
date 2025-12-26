namespace Application.Interfaces.HashManagement
{
    public interface IHashManager   //interface for Hash management
    {
        string HashPassword(string password);   //method to Hash user Password
        (string plain ,string hashed)  GenerateHashedToken();   //method to generate hashed Guid as Token
        bool VerifyHashedValue(string PlainText, string HashedValue);   //method to verify a PlainText with Hashed value
    }
}
