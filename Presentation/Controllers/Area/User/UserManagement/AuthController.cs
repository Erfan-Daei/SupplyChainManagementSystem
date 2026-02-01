using Application.Services.MediatR.Commands.User.UserManagement.LogIn;
using Application.Services.MediatR.Commands.User.UserManagement.LogOut;
using Application.Services.MediatR.Commands.User.UserManagement.RefreshToken;
using Application.Services.MediatR.Commands.User.UserManagement.SignUp;
using Common.Output;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Area.User.UserManagement.Auth;
using Presentation.Output.Base;

namespace Presentation.Controllers.Area.User.UserManagement
{
    [Area("User")]
    [Route("api/[area]/UserManagement/[controller]")]   //api/{area}/{prefix}/{controller}
    [ApiController]
    public class AuthController : ControllerBase   //controller for Authentication
    {
        private readonly IMediator _mediator;   //mediator pattern
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("SingUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpCommandRequest request)
        {
            //create User and UserInRole and then "get" UserId to api for confirmation proccess
            var result = await _mediator.Send(new SignUpCommand(request, User));

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiSignUpResult.Result(request.Dto.UserEmail!, request.Dto.Password!, result.Data, Url)
            });
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn([FromBody] LogInCommand request)
        {
            //LogIn user and create Jwt Token and Refresh Token
            var result = await _mediator.Send(request);

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiLogInResult.Result(result.Data!, Url)
            });
        }

        [Authorize]
        [HttpPost("LogOut", Name = "LogOut")]
        public async Task<IActionResult> LogOut([FromBody] LogOutCommandRequest request)
        {
            var result = await _mediator.Send(new LogOutCommand(request, User.Claims));

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = null,
            });
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand request)
        {
            //regenerate Jwt Token and Refresh Token
            var result = await _mediator.Send(request);

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiRefreshTokenResult.Result(result.Data!)
            });
        }
    }
}
