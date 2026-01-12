using System.Net.Mail;
using System.Text;


namespace Infrastructure.EmailManagement.Requirements.SmtpMessageProvider
{
    public class SmtpMessageProvider : ISmtpMessageProvider
    {
        public MailMessage ConfirmationEmailMessage(string userName, string userEmail, string subject, string template)
        {
            var message = new MailMessage(userName, userEmail, subject, template)
            {
                IsBodyHtml = true,
                BodyEncoding = UTF8Encoding.UTF8,
                DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess,
            };

            return message;
        }
    }
}
