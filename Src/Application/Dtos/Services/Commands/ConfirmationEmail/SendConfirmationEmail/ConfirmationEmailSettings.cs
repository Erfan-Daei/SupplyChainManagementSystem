namespace Application.Dtos.Services.Commands.ConfirmationEmail.SendConfirmationEmail
{
    //POCO class to bind ConfirmationEmailSettings from appsettings.json
    public class ConfirmationEmailSettings
    {
        public string Subject { get; set; }
        public string ActivationLink { get; set; }
        public int UserTokenExpireMinutes { get; set; }
    }
}
