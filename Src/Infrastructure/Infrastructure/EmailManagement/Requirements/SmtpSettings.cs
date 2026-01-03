namespace Infrastructure.EmailManagement.Requirements
{
    //POCO class to bind data from appsettings.json
    public class SmtpSettings
    {
        public string Host { get; set; } = null!;
        public int Port { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string RecipientEmail { get; set; } = null!;

    }
}
