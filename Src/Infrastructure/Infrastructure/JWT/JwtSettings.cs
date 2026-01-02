namespace Infrastructure.JWT
{
    //POCO class to bind data from appsettings.json to get JwtSettings
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpireMinutes { get; set; }
    }
}
