using Application.Services.MediatR.Commands.User.UserManagement.LogIn;
using Application.Services.MediatR.Commands.User.UserManagement.LogOut;
using Application.Services.MediatR.Commands.User.UserManagement.RefreshToken;
using Application.Services.MediatR.Commands.User.UserManagement.SignUp;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Area.User.UserManagement;
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
        public async Task<IActionResult> SignUp([FromBody] SignUpCommand request)
        {
            //create User and UserInRole and then "get" UserId to api for confirmation proccess
            var signUpResult = await _mediator.Send(request);

            return CreatedAtRoute("SendConfirmationEmail",
                new { Area = "User", userId = signUpResult.Data },
                new ApiResultDto()   //body
                {
                    IsSuccess = signUpResult.IsSuccess,
                    Message = signUpResult.Message,
                    StatusCode = signUpResult.StatusCode,
                    Links = []
                }
            );
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn([FromBody] LogInCommand request)
        {
            //LogIn user and create Jwt Token and Refresh Token
            var result = await _mediator.Send(request);

            return Ok(new ApiResultDto<ApiJwtTokenDto>
            {
                Data = new ApiJwtTokenDto
                {
                    AccessToken = result.Data?.AccessToken ?? string.Empty,
                    RefreshToken = result.Data?.RefreshToken ?? string.Empty,
                },
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Links = []
            });
        }

        [HttpPut("LogOut", Name ="LogOut")]
        public async Task<IActionResult> LogOut(LogOutCommandRequest request)
        {
            var result = await _mediator.Send(new LogOutCommand(request, User.Claims));

            return Ok(new ApiResultDto
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Links = []
            });
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromHeader] RefreshTokenCommand request)
        {
            //regenerate Jwt Token and Refresh Token
            var result = await _mediator.Send(request);

            return Ok(new ApiResultDto<ApiJwtTokenDto>
            {
                Data = new ApiJwtTokenDto
                {
                    AccessToken = result.Data?.AccessToken ?? string.Empty,
                    RefreshToken = result.Data?.RefreshToken ?? string.Empty,
                },
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Links = []
            });
        }
    }
}
