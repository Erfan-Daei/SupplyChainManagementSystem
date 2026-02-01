using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Base;

namespace Presentation.Output.Area.User.UserManagement.Email
{
    public class ApiVerifyConfirmationEmailResult
    {
        public List<LinkDto> Links { get; set; } = [];

        public static ApiVerifyConfirmationEmailResult Result(Guid userId, IUrlHelper url)
        {
            return new ApiVerifyConfirmationEmailResult
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
