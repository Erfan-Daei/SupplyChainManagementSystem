using Application.Services.MediatR.Commands.User.UserManagement.ChangePassword.RequestChangePassword;
using Application.Services.MediatR.Commands.User.UserManagement.ChangePassword.VerifyChangePassword;
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
        [HttpPost("RequestChangePassword")]
        public async Task<IActionResult> RequestChangePassword([FromQuery] RequestChangePasswordCommandRequest request)
        {
            var result = await _mediator.Send(new RequestChangePasswordCommand(request, User.Claims));

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiRequestChangePasswordResult.Result(Url)
            });
        }

        [Authorize]
        [HttpPost("VerifyChangePassword")]
        public async Task<IActionResult> VerifyChangePassword([FromQuery] VerifyChangePasswordCommandRequest request)
        {
            var result = await _mediator.Send(new VerifyChangePasswordCommand(request, User.Claims));

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiVerifyChangePasswordResult.Result(Url)
            });
        }
    }
}
