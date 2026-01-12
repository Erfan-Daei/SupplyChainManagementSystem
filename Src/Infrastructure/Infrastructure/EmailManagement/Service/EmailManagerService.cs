using Application.Dtos.EmailManagement;
using Application.Interfaces.EmailManagement;
using Common.Output;
using Infrastructure.EmailManagement.ExceptionHandler;
using Infrastructure.EmailManagement.Requirements;
using Infrastructure.EmailManagement.Requirements.SmtpClientConfiguration;
using Infrastructure.EmailManagement.Requirements.SmtpMessageProvider;
using Infrastructure.EmailManagement.Requirements.TemplateProvider;
using System.Net;

namespace Infrastructure.EmailManagement.Service
{
    //class to send Email To User
    public class EmailManagerService : IEmailManager
    {
        //inject POCO class to bind data from appsetting.json
        private readonly SmtpSettings _smtpSettings;
        private readonly ITemplateProvider _templateProvider;   //ConfirmationEmailTemplate
        private readonly ISmtpClientConfiguration _smtpClientConfiguration;   //ConfigureSmtpClient
        private readonly ISmtpMessageProvider _smtpMessageProvider; //ConfirmationEmailMessage
        public EmailManagerService(SmtpSettings smtpSettings,
            ITemplateProvider templateProvider,
            ISmtpClientConfiguration smtpClientConfiguration,
            ISmtpMessageProvider smtpMessageProvider)
        {
            _smtpSettings = smtpSettings;
            _templateProvider = templateProvider;
            _smtpClientConfiguration = smtpClientConfiguration;
            _smtpMessageProvider = smtpMessageProvider;
        }

        public async Task<ResultDto> ConfirmationEmailSenderAsync(ConfirmationEmailSenderRequestDto request)
        {
            try
            {
                //configure SmtpClient
                using var client = _smtpClientConfiguration.ConfigureSmtpClient(
                    _smtpSettings.Host,
                    _smtpSettings.Port,
                    _smtpSettings.UserName,
                    _smtpSettings.Password
                );

                //get template for sending email and add Users UserFullName and Email with ActivationLink
                //Template will copy to output directory
                var template = await _templateProvider.ConfirmationEmailTemplate(request, _smtpSettings.UserName);

                //configure message 
                using var message = _smtpMessageProvider.ConfirmationEmailMessage(
                    _smtpSettings.UserName,
                    request.UserEmail,
                    request.Subject,
                    template
                );

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
                return EmailManagerExceptionHandler.Handle(ex);   //custom Exception handler for Smtp Exceptions
            }
        }
    }
}
