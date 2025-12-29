using Application.Dtos.EmailManagement;
using Infrastructure.Interfaces.EmailManagement.Requirements;

namespace Infrastructure.EmailManagement.Requirements
{
    public class TemplateProvider : ITemplateProvider
    {
        private readonly ConfirmationEmailPath _confirmationEmailPath;
        public TemplateProvider(ConfirmationEmailPath confirmationEmailPath)
        {
            _confirmationEmailPath = confirmationEmailPath;
        }
        public async Task<string> ConfirmationEmailTemplate(ConfirmationEmailSenderRequestDto request, string smtpSettingsUserName)
        {
            var TemplatePath = Path.Combine(AppContext.BaseDirectory, _confirmationEmailPath.Path);
            var Template = await File.ReadAllTextAsync(TemplatePath);
            var TemplateBody = Template
                .Replace("{Subject}", request.Subject)
                .Replace("{UserFullName}", request.UserFullName)
                .Replace("{ActivationLink}", request.ActivationLink)
                .Replace("{AdminEmail}", smtpSettingsUserName);

            return TemplateBody;
        }
    }
}
