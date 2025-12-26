using Application.Interfaces.Services.Commands.ConfirmationEmail;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output;

namespace Presentation.Controllers.Area.User.UserManagement
{
    [Area("User")]
    [Route("api/[area]/UserManagement/[controller]")]  //api/{area}/{prefix}/{controller}
    [ApiController]
    public class EmailManagerController : ControllerBase
    {
        private readonly ISendConfirmationEmail _sendConfirmationEmail;
        public EmailManagerController(ISendConfirmationEmail sendConfirmationEmail)
        {
            _sendConfirmationEmail = sendConfirmationEmail;
        }

        [HttpPost(Name = "SendConfirmationEmail")]
        public async Task<IActionResult> SendConfirmationEmail([FromQuery] Guid userId)
        {
            var sendConfirmationEmailResult = await _sendConfirmationEmail.SendConfirmationEmail(userId);

            if (!sendConfirmationEmailResult.IsSuccess)
                return Problem(sendConfirmationEmailResult.Message, string.Empty, Convert.ToInt16(sendConfirmationEmailResult.StatusCode));

            return Ok(new ApiResultDto()
            {
                IsSuccess = sendConfirmationEmailResult.IsSuccess,
                Message = sendConfirmationEmailResult.Message,
                StatusCode = sendConfirmationEmailResult.StatusCode,
                Links = new List<LinkDto>()
            });
        }
    }
}
