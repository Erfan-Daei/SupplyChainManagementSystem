namespace Application.Dtos.JWT
{
    //POCO class to bind data from appsetings.json to get RefreshTokenSettings for RefreshToken Proccess
    public class RefreshTokenSettings
    {
        public string SecretKey { get; set; } = null!;
        public int ExpireDays { get; set; }
    }
}
