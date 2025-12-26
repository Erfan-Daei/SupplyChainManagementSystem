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
        private readonly IVerifyConfirmationEmail _verifyConfirmationEmail;
        public EmailManagerController(ISendConfirmationEmail sendConfirmationEmail,
            IVerifyConfirmationEmail verifyConfirmationEmail)
        {
            _sendConfirmationEmail = sendConfirmationEmail;
            _verifyConfirmationEmail = verifyConfirmationEmail;
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

        [HttpGet]
        public async Task<IActionResult> VerifyConfirmationEmail([FromQuery] Guid userId, string userToken)
        {
            var verifyConfirmationResult = await _verifyConfirmationEmail.VerifyConfirmationEmailAsync(userId, userToken);

            if (!verifyConfirmationResult.IsSuccess)
                return Problem(verifyConfirmationResult.Message, string.Empty, Convert.ToInt16(verifyConfirmationResult.StatusCode));

            return Ok(new ApiResultDto()
            {
                IsSuccess = verifyConfirmationResult.IsSuccess,
                Message = verifyConfirmationResult.Message,
                StatusCode = verifyConfirmationResult.StatusCode,
                Links = new List<LinkDto>()
            });
        }
    }
}
