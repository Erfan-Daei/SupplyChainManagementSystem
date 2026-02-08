using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Base;

namespace Presentation.Output.Area.User.UserManagement.ChangePassword
{
    public class ApiRequestChangePasswordResult
    {
        public List<LinkDto> Links { get; set; } = [];

        public static ApiRequestChangePasswordResult Result(IUrlHelper url)
        {
            return new ApiRequestChangePasswordResult
            {
                Links = new List<LinkDto>
                {
                    new LinkDto
                    {
                        Href = url.Action("VerifyChangePassword", "ChangePassword", new {userId = "UserId", token = "Token"})!,
                        Method = "POST",
                        Rel = "Verify"
                    }
                }
            };
        }
    }
}
