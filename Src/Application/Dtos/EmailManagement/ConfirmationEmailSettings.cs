namespace Application.Dtos.EmailManagement
{
    //POCO class to bind ConfirmationEmailSettings from appsettings.json For ConfirmationEmailService Proccess
    public class ConfirmationEmailSettings
    {
        public string Subject { get; set; } = null!;
        public string ActivationLink { get; set; } = null!;
        public int UserTokenExpireMinutes { get; set; }
    }
}
