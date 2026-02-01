using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Base;

namespace Presentation.Output.Area.User.UserManagement.Users
{
    public class ApiAssignCompanyToUserResult
    {
        public List<LinkDto> Links { get; set; } = [];

        public static ApiAssignCompanyToUserResult Result(Guid userId, Guid companyId, IUrlHelper url)
        {
            return new ApiAssignCompanyToUserResult
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
                        Href = url.ActionLink("GetCompanyDetail", "CompanyManager", new {companyId = companyId})!,
                        Method = "GET" ,
                        Rel = "CompanyDetail"
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
