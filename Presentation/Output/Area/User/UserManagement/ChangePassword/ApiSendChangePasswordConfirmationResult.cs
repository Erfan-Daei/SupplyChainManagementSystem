using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Base;

namespace Presentation.Output.Area.User.UserManagement.ChangePassword
{
    public class ApiSendChangePasswordConfirmationResult
    {
        public List<LinkDto> Links { get; set; } = [];

        public static ApiSendChangePasswordConfirmationResult Result(IUrlHelper url)
        {
            return new ApiSendChangePasswordConfirmationResult
            {
                Links = new List<LinkDto>
                {
                    new LinkDto
                    {
                        Href = url.Action("VerifyChangePasswordConfirmation", new {userId = "UserId", token = "Token"})!,
                        Method = "POST",
                        Rel = "Verify"
                    }
                }
            };
        }
    }
}
