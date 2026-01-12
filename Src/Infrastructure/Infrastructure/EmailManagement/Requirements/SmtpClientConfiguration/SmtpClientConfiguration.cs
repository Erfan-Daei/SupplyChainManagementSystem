using System.Net;
using System.Net.Mail;

namespace Infrastructure.EmailManagement.Requirements.SmtpClientConfiguration
{
    public class SmtpClientConfiguration : ISmtpClientConfiguration
    {
        public SmtpClient ConfigureSmtpClient(string host, int port, string userName, string password)
        {
            var client = new SmtpClient(host, port)
            {
                EnableSsl = true,
                Timeout = 60000,   //1 minutes timeout
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,   //use my Credential
                Credentials = new NetworkCredential(userName, password)   //give my UserName and Password to make Credential
            };
            return client;
        }
    }
}
