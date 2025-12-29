using Application.MediatR.Services.Commands.SignInService;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output;

namespace Presentation.Controllers.Area.User.UserManagement
{
    [Area("User")]
    [Route("api/[area]/UserManagement/[controller]")]   //api/{area}/{prefix}/{controller}
    [ApiController]
    public class SignInController : ControllerBase
    {
        private readonly IMediator _mediator;   //mediator pattern
        public SignInController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInCommand request)
        {
            //create User and UserInRole and then "get" UserId to api for confirmation proccess
            var signInResult = await _mediator.Send(request);

            return CreatedAtRoute("SendConfirmationEmail",
                new { Area = "User", userId = signInResult.Data },
                new ApiResultDto()   //body
                {
                    IsSuccess = true,
                    Message = signInResult.Message,
                    StatusCode = signInResult.StatusCode,
                    Links = []
                }
            );
        }
    }
}
