using System.Net.Mail;

namespace Infrastructure.Interfaces.EmailManagement.Requirements
{
    public interface ISmtpMessageProvider
    {
        MailMessage ConfirmationEmailMessage(string userName, string userEmail, string subject, string template);
    }
}
