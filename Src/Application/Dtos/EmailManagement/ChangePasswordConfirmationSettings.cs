namespace Application.Dtos.EmailManagement
{
    //POCO class to bind ChangePasswordConfirmationSettings from appsettings.json For ChangePasswordConfirmation Service Proccess
    public class ChangePasswordConfirmationSettings
    {
        public string Subject { get; set; } = null!;
        public int UserTokenExpireMinutes { get; set; }
    }
}
