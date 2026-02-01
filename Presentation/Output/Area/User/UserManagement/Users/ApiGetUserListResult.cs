using Application.Services.Implement.Queries.Users.UserManagement.GetUserList;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Base;

namespace Presentation.Output.Area.User.UserManagement.Users
{
    public class ApiGetUserListValues
    {
        public Guid UserId { get; set; }
        public string UserFullName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
    }

    public class ApiGetUserListResult
    {
        public List<ApiGetUserListValues> Values { get; set; } = [];
        public List<LinkDto> Links { get; set; } = [];

        public static ApiGetUserListResult Result(List<GetUserListResultDto>? resultDto, IUrlHelper url)
        {
            if (resultDto == null)
                return new ApiGetUserListResult();

            return new ApiGetUserListResult
            {
                Values = resultDto.Select(u => new ApiGetUserListValues
                {
                    UserId = u.UserId,
                    UserFullName = u.UserFullName,
                    UserEmail = u.UserEmail,
                }).ToList(),
                Links = new List<LinkDto>()
                {
                    new LinkDto
                    {
                        Href = url.ActionLink("GetUserDetail", "UserManager", new {userId = "guid - userId"})!,
                        Method = "GET" ,
                        Rel = "Self"
                    },
                    new LinkDto
                    {
                        Href = url.ActionLink("SendConfirmationEmail", "EmailManager", new {userId = "guid - userId"})!,
                        Method = "POST" ,
                        Rel = "ConfirmEmail"
                    },
                    new LinkDto
                    {
                        Href = url.ActionLink("PromoteUserRole", "RoleManager", new {userId = "guid - userId"})!,
                        Method = "POST" ,
                        Rel = "PromoteRole"
                    },
                    new LinkDto
                    {
                        Href = url.ActionLink("DemoteUserRole", "RoleManager", new {userId = "guid - userId"})!,
                        Method = "POST" ,
                        Rel = "DemoteRole"
                    },
                }
            };
        }
    }
}
