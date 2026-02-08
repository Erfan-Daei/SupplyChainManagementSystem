using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Base;

namespace Presentation.Output.Area.User.UserManagement.ChangePassword
{
    public class ApiVerifyChangePasswordResult
    {
        public List<LinkDto> Links { get; set; } = [];

        public static ApiVerifyChangePasswordResult Result(IUrlHelper url)
        {
            return new ApiVerifyChangePasswordResult
            {
                Links = new List<LinkDto>
                {
                    new LinkDto
                    {
                        Href = url.Action("GetUserDetail", "UserManagement", new {userId = "UserId"})!,
                        Method = "GET",
                        Rel = "Self"
                    }
                }
            };
        }
    }
}
