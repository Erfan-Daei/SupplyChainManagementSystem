using System.Net.Mail;

namespace Infrastructure.EmailManagement.Requirements.SmtpMessageProvider
{
    public interface ISmtpMessageProvider
    {
        MailMessage ConfirmationEmailMessage(string userName, string userEmail, string subject, string template);
    }
}
