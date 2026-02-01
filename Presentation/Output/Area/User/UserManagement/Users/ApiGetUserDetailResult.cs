using Application.Services.Implement.Queries.Users.UserManagement.GetUserDetail;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Base;

namespace Presentation.Output.Area.User.UserManagement.Users
{
    public class ApiGetUserDetailResult
    {
        public string UserFullName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string UserCompanyName { get; set; } = null!;
        public Guid UserCompanyId { get; set; }
        public string UserRole { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public List<LinkDto> Links { get; set; } = [];

        public static ApiGetUserDetailResult Result(Guid userId, GetUserDetailResultDto? resultDto, IUrlHelper url)
        {
            if (resultDto == null)
                return new ApiGetUserDetailResult();

            return new ApiGetUserDetailResult
            {
                UserFullName = resultDto.UserFullName,
                UserEmail = resultDto.UserEmail,
                UserCompanyName = resultDto.UserCompanyName,
                UserCompanyId = resultDto.UserCompanyId,
                UserRole = resultDto.UserRole,
                CreatedAt = resultDto.CreatedAt,
                Links = new List<LinkDto>()
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
                        Href = url.ActionLink("DemoteUserRole", "RoleManager", new {userId = userId})!,
                        Method = "POST" ,
                        Rel = "DemoteRole"
                    },
                     new LinkDto
                    {
                        Href = url.ActionLink("GetCompanyDetail", "CompanyManager", new {companyId = resultDto.UserCompanyId})!,
                        Method = "GET" ,
                        Rel = "CompanyDetail"
                    },
                }
            };
        }
    }
}
