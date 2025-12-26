using Application.Interfaces.Services.Commands.SignIn;
using Application.Services.Commands.SignIn;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output;

namespace Presentation.Controllers.Area.User.UserManagement
{
    [Area("User")]
    [Route("api/[area]/UserManagement/[controller]")]   //api/{area}/{prefix}/{controller}
    [ApiController]
    public class RegisterationManagerController : ControllerBase
    {
        private readonly ISignIn _signIn;
        public RegisterationManagerController(ISignIn signIn)
        {
            _signIn = signIn;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInServiceRequestDto request)
        {
            //create User and UserInRole and then "get" UserId to api for confirmation proccess
            var signInResult = await _signIn.CreateUserAsync(request);
            if (!signInResult.IsSuccess)
                return Problem(signInResult.Message, string.Empty, Convert.ToInt16(signInResult.StatusCode));

            return CreatedAtRoute("SendConfirmationEmail",
                new { Area = "User", userId = signInResult.Data },
                new ApiResultDto()   //body
                {
                    IsSuccess = true,
                    Message = signInResult.Message,
                    StatusCode = signInResult.StatusCode,
                    Links = new List<LinkDto>()
                }
            );
        }
    }
}
