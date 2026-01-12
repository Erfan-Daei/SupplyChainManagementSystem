namespace Application.Dtos.EmailManagement
{
    //POCO class to bind ConfirmationEmailSettings from appsettings.json
    public class ConfirmationEmailSettings
    {
        public string Subject { get; set; } = null!;
        public string ActivationLink { get; set; } = null!;
        public int UserTokenExpireMinutes { get; set; }
    }
}
