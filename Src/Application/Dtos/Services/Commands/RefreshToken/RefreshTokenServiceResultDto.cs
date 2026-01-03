namespace Application.Dtos.Services.Commands.RefreshToken
{
    public class RefreshTokenServiceResultDto
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTime RefreshTokenExpirationTime { get; set; }
    }
}
