using Application.Services.MediatR.Commands.User.UserManagement.ChangePassword.ChangePasswordConfirmation.SendChangePasswordConfirmation;
using Common.Output;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Area.User.UserManagement.ChangePassword;
using Presentation.Output.Base;

namespace Presentation.Controllers.Area.User.UserManagement
{
    [Area("User")]
    [Route("api/[area]/UserManagement/[controller]")]   //api/{area}/{prefix}/{controller}
    [ApiController]
    public class ChangePasswordController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ChangePasswordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("SendChangePasswordConfirmation")]
        public async Task<IActionResult> SendChangePasswordConfirmation([FromQuery] SendChangePasswordConfirmationCommandRequest request)
        {
            var result = await _mediator.Send(new SendChangePasswordConfirmationCommand(request, User.Claims));

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiSendChangePasswordConfirmationResult.Result(Url)
            });
        }

        [Authorize]
        [HttpPost("VerifyChangePasswordConfirmation")]
        public async Task<IActionResult> VerifyChangePasswordConfirmation([FromQuery]  request)
        {
            var result = await _mediator.Send();

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = null
            });
        }
    }
}
