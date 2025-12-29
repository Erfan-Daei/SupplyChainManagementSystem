using Application.Dtos.EmailManagement;

namespace Infrastructure.Interfaces.EmailManagement.Requirements
{
    public interface ITemplateProvider
    {
        Task<string> ConfirmationEmailTemplate(ConfirmationEmailSenderRequestDto request, string smtpSettingsUserName);
    }
}
