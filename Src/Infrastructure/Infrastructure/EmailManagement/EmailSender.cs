using Application.Interfaces.EmailManagement;
using Common.Output;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Infrastructure.EmailManagement
{
    //class to send Email To User
    public class EmailSender : IEmailSender
    {
        //inject POCO class to bind data from appsetting.json
        private readonly SmtpSettings _smtpSettings;
        public EmailSender(SmtpSettings smtpSettings)
        {
            _smtpSettings = smtpSettings;
        }

        public async Task<ResultDto> ConfirmationEmailSenderAsync(ConfirmationEmailSenderRequestDto request)
        {
            try
            {
                //configure SmtpClient
                using var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
                {
                    EnableSsl = true,
                    Timeout = 60000,   //1 minutes timeout
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,   //use my Credential
                    Credentials = new NetworkCredential(_smtpSettings.UserName, _smtpSettings.Password)   //give my UserName and Password to make Credential
                };

                //get template for sending email and add Users UserFullName and Email with ActivationLink
                var TemplatePath = Path.Combine(AppContext.BaseDirectory, "EmailManagement", "Template", "ConfirmationEmailSenderTemplate.html");
                var Template = await File.ReadAllTextAsync(TemplatePath);
                var TemplateBody = Template
                    .Replace("{Subject}", request.Subject)
                    .Replace("{UserFullName}", request.UserFullName)
                    .Replace("{ActivationLink}", request.ActivationLink)
                    .Replace("{AdminEmail}", _smtpSettings.UserName);

                //configure message 
                using var message = new MailMessage(_smtpSettings.UserName, request.UserEmail, request.Subject, TemplateBody)
                {
                    IsBodyHtml = true,
                    BodyEncoding = UTF8Encoding.UTF8,
                    DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess,
                };

                await client.SendMailAsync(message);

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "ایمیل با موفقیت ارسال شد",
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return EmailSenderExceptionHandler.Handle(ex);   //custom Exception handler for Smtp Exceptions
            }
        }
    }
}
