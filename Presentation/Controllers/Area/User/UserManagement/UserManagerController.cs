using Application.Services.MediatR.Commands.Admin.UserManagement.AssignCompanyToUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Base;

namespace Presentation.Controllers.Area.User.UserManagement
{
    [Area("User")]
    [Route("api/[area]/UserManagement/[controller]")]
    [ApiController]
    public class UserManagerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserManagerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("AssignCompanyToUser")]
        public async Task<IActionResult> AssignCompanyToUser([FromBody] AssignCompanyToUserCommandRequest request)
        {
            var result = await _mediator.Send(new AssignCompanyToUserCommand(request, User.Claims));

            return Ok(new ApiResultDto
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Links = []
            });
        }
    }
}
