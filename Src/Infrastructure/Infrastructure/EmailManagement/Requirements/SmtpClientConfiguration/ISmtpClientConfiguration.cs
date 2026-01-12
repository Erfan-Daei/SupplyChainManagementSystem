using System.Net.Mail;

namespace Infrastructure.EmailManagement.Requirements.SmtpClientConfiguration
{
    public interface ISmtpClientConfiguration
    {
        SmtpClient ConfigureSmtpClient(string host, int port, string userName, string password);
    }
}
