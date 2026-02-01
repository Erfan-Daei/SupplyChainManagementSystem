using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Base;

namespace Presentation.Output.Area.User.UserManagement.Auth
{
    public class ApiSignUpResult
    {
        public Guid UserId { get; set; }
        public List<LinkDto> Links { get; set; } = [];

        public static ApiSignUpResult Result(string userEmail, string userPassword, Guid? userId, IUrlHelper url)
        {
            if (userId == null)
                return new ApiSignUpResult();

            return new ApiSignUpResult
            {
                UserId = userId ?? Guid.Empty,
                Links = new List<LinkDto>
                {
                    new LinkDto
                    {
                        Href = url.ActionLink("LogIn", "Auth", new {UserEmail = userEmail, UserPassword = userPassword})!,
                        Method = "POST" ,
                        Rel = "LogIn"
                    },
                    new LinkDto
                    {
                        Href = url.ActionLink("GetUserDetail", "UserManager", new {userId = userId})!,
                        Method = "GET" ,
                        Rel = "Self"
                    },
                    new LinkDto
                    {
                        Href = url.ActionLink("SendConfirmationEmail", "EmailManager", new {userId = userId})!,
                        Method = "POST" ,
                        Rel = "ConfirmEmail"
                    },
                    new LinkDto
                    {
                        Href = url.ActionLink("PromoteUserRole", "RoleManager", new {userId = userId})!,
                        Method = "POST" ,
                        Rel = "PromoteRole"
                    },
                    new LinkDto
                    {
                        Href = url.ActionLink("DemoteUserRole", "RoleManager", new {userId = userId})!,
                        Method = "POST" ,
                        Rel = "DemoteRole"
                    }
                }
            };
        }
    }
}
