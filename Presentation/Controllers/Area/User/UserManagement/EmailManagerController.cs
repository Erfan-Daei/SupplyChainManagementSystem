using Application.MediatR.Services.Commands.ConfirmationEmail.SendConfirmationEmail;
using Application.MediatR.Services.Commands.ConfirmationEmail.VerifyConfirmationEmail;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Base;

namespace Presentation.Controllers.Area.User.UserManagement
{
    [Area("User")]
    [Route("api/[area]/UserManagement/[controller]")]  //api/{area}/{prefix}/{controller}
    [ApiController]
    public class EmailManagerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmailManagerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("SendConfirmationEmail", Name = "SendConfirmationEmail")]
        public async Task<IActionResult> SendConfirmationEmail([FromQuery] SendConfirmationEmailCommand request)
        {
            var sendConfirmationEmailResult = await _mediator.Send(request);

            return Ok(new ApiResultDto()
            {
                IsSuccess = sendConfirmationEmailResult.IsSuccess,
                Message = sendConfirmationEmailResult.Message,
                StatusCode = sendConfirmationEmailResult.StatusCode,
                Links = []
            });
        }

        [HttpGet("VerifyConfirmationEmail")]
        public async Task<IActionResult> VerifyConfirmationEmail([FromQuery] VerifyConfirmationEmailCommand request)
        {
            var verifyConfirmationResult = await _mediator.Send(request);

            return Ok(new ApiResultDto()
            {
                IsSuccess = verifyConfirmationResult.IsSuccess,
                Message = verifyConfirmationResult.Message,
                StatusCode = verifyConfirmationResult.StatusCode,
                Links = []
            });
        }
    }
}
