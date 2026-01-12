using Application.Services.MediatR.Commands.User.LogIn;
using Application.Services.MediatR.Commands.User.LogOut;
using Application.Services.MediatR.Commands.User.RefreshToken;
using Application.Services.MediatR.Commands.User.SignUp;
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
            var result = await _mediator.Send(request);

            //add http-only ResreshToken
            if (result.Data != null)
                Response.Cookies.Append("RefreshToken", result.Data.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = result.Data.RefreshTokenExpireTime
                });

            return Ok(new ApiResultDto<ApiJwtTokenDto>
            {
                Data = new ApiJwtTokenDto { AccessToken = result.Data?.AccessToken ?? string.Empty },
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Links = []
            });
        }

        [HttpPut("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            var refreshToken = Request.Cookies["RefreshToken"];
            if (refreshToken == null)
                return Unauthorized("توکن یافت نشد");

            var result = await _mediator.Send(new LogOutCommand(refreshToken));

            return Ok(new ApiResultDto
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Links = []
            });
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["RefreshToken"];
            if (refreshToken == null)
                return Unauthorized("توکن یافت نشد");

            var result = await _mediator.Send(new RefreshTokenCommand(refreshToken));

            if (result.IsSuccess)
                Response.Cookies.Append("RefreshToken", result.Data!.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = result.Data.RefreshTokenExpirationTime
                });

            return Ok(new ApiResultDto<ApiJwtTokenDto>
            {
                Data = new ApiJwtTokenDto
                {
                    AccessToken = result.Data?.AccessToken ?? string.Empty,
                },
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Links = []
            });
        }
    }
}
