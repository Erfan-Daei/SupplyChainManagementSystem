using Application.Dtos.EmailManagement;

namespace Infrastructure.EmailManagement.Requirements.TemplateProvider
{
    public interface ITemplateProvider
    {
        Task<string> ConfirmationEmailTemplate(ConfirmationEmailSenderRequestDto request, string smtpSettingsUserName);
    }
}
