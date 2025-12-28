using System.Net.Mail;

namespace Infrastructure.Interfaces.EmailManagement.Requirements
{
    public interface ISmtpClientConfiguration
    {
        SmtpClient ConfigureSmtpClient(string host, int port, string userName, string password);
    }
}
