using Application.Services.MediatR.Commands.User.UserManagement.ConfirmationEmail.SendConfirmationEmail;
using Application.Services.MediatR.Commands.User.UserManagement.ConfirmationEmail.VerifyConfirmationEmail;
using Common.Output;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Area.User.UserManagement.Email;
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
            var result = await _mediator.Send(request);

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = null,
            });
        }

        [HttpGet("VerifyConfirmationEmail")]
        public async Task<IActionResult> VerifyConfirmationEmail([FromQuery] VerifyConfirmationEmailCommand request)
        {
            var result = await _mediator.Send(request);

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiVerifyConfirmationEmailResult.Result(request.userId, Url)
            });
        }
    }
}
