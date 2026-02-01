using Application.Services.Implement.Commands.Users.UserManagement.RefreshToken;

namespace Presentation.Output.Area.User.UserManagement.Auth
{
    public class ApiRefreshTokenResult
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;

        public static ApiRefreshTokenResult Result(RefreshTokenServiceResultDto? resultDto)
        {
            if (resultDto == null)
                return new ApiRefreshTokenResult();

            return new ApiRefreshTokenResult
            {
                AccessToken = resultDto.AccessToken,
                RefreshToken = resultDto.RefreshToken
            };
        }
    }
}
