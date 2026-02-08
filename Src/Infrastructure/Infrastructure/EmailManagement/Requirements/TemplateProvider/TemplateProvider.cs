using Application.Dtos.EmailManagement;

namespace Infrastructure.EmailManagement.Requirements.TemplateProvider
{
    public class TemplateProvider : ITemplateProvider
    {
        private readonly ConfirmationEmailPath _confirmationEmailPath;
        private readonly ChangePasswordConfirmationPath _changePasswordConfirmationPath;
        public TemplateProvider(ConfirmationEmailPath confirmationEmailPath
            , ChangePasswordConfirmationPath changePasswordConfirmationPath)
        {
            _confirmationEmailPath = confirmationEmailPath;
            _changePasswordConfirmationPath = changePasswordConfirmationPath;
        }
        public async Task<string> ConfirmationEmailTemplate(ConfirmationEmailSenderRequestDto request, string smtpSettingsUserName)
        {
            string TemplatePath = string.Empty;

            switch (request.EmailSenderType)
            {
                case ConfirmationEmailSenderType.EmailConfirmation:
                    TemplatePath = Path.Combine(AppContext.BaseDirectory, _confirmationEmailPath.Path);
                    break;

                case ConfirmationEmailSenderType.ChangePasswordConfirmation:
                    TemplatePath = Path.Combine(AppContext.BaseDirectory, _changePasswordConfirmationPath.Path);
                    break;
            }

            var Template = await File.ReadAllTextAsync(TemplatePath);
            var TemplateBody = Template
                .Replace("{Subject}", request.Subject)
                .Replace("{UserFullName}", request.UserFullName)
                .Replace("{Value}", request.Value)
                .Replace("{AdminEmail}", smtpSettingsUserName);

            return TemplateBody;
        }
    }
}
