using Application.MediatR.Services.Commands.SignUp;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output;

namespace Presentation.Controllers.Area.User.UserManagement
{
    [Area("User")]
    [Route("api/[area]/UserManagement/[controller]")]   //api/{area}/{prefix}/{controller}
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly IMediator _mediator;   //mediator pattern
        public SignUpController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpCommand request)
        {
            //create User and UserInRole and then "get" UserId to api for confirmation proccess
            var signUpResult = await _mediator.Send(request);

            return CreatedAtRoute("SendConfirmationEmail",
                new { Area = "User", userId = signUpResult.Data },
                new ApiResultDto()   //body
                {
                    IsSuccess = true,
                    Message = signUpResult.Message,
                    StatusCode = signUpResult.StatusCode,
                    Links = []
                }
            );
        }
    }
}
