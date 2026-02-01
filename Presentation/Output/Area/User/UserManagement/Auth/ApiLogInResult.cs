using Application.Services.Implement.Commands.Users.UserManagement.LogIn;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Base;

namespace Presentation.Output.Area.User.UserManagement.Auth
{
    public class ApiLogInResult
    {
        public string AcceessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public List<LinkDto> Links { get; set; } = [];

        public static ApiLogInResult Result(LogInServiceResultDto? resultDto, IUrlHelper url)
        {
            if (resultDto == null)
                return new ApiLogInResult();

            return new ApiLogInResult
            {
                AcceessToken = resultDto.AccessToken,
                RefreshToken = resultDto.RefreshToken,
                Links = new List<LinkDto>
                {
                    new LinkDto
                    {
                        Href = url.ActionLink("LogOut", "Auth", new {})!,
                        Method = "POST" ,
                        Rel = "LogOut"
                    },
                    new LinkDto
                    {
                        Href = url.ActionLink("RefreshToken", "Auth", new {refreshToken = resultDto?.RefreshToken ?? null})!,
                        Method = "POST" ,
                        Rel = "RefreshToken"
                    },
                }
            };
        }
    }
}
