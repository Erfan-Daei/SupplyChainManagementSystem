using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Base;

namespace Presentation.Output.Area.User.UserManagement.Role
{
    public class ApiChangeUserRoleResult
    {
        public List<LinkDto> Links { get; set; } = [];

        public static ApiChangeUserRoleResult Result(Guid userId, IUrlHelper url)
        {
            return new ApiChangeUserRoleResult
            {
                Links = new List<LinkDto>
                {
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
                        Href = url.ActionLink("LogOut", "Auth", new {userId = userId})!,
                        Method = "POST" ,
                        Rel = "LogOut"
                    }
                }
            };
        }
    }
}
