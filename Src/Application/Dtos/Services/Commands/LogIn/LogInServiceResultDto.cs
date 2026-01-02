namespace Application.Dtos.Services.Commands.LogIn
{
    public class LogInServiceResultDto
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTime RefreshTokenExpireTime { get; set; }
    }
}
