namespace Infrastructure.EmailManagement.Requirements
{
    //POCO class to bind data from appsettings.json
    public class SmtpSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RecipientEmail { get; set; }

    }
}
