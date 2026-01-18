using Application.Services.MediatR.Commands.Admin.UserManagement.DemoteUserRole;
using Application.Services.MediatR.Commands.Admin.UserManagement.PromoteUserRole;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Base;

namespace Presentation.Controllers.Area.User.UserManagement
{
    [Area("Admin")]
    [Route("api/[area]/UserManagement/[controller]")]
    [ApiController]
    public class RoleManagerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RoleManagerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("PromoteUserRole")]
        public async Task<IActionResult> PromoteUserRole([FromBody] PromoteUserRoleCommandRequest request)
        {
            var result = await _mediator.Send(new PromoteUserRoleCommand(request, User.Claims));

            return AcceptedAtRoute("LogOut",
                new { Area = "User", userId = result.Data },
                new ApiResultDto
                {
                    IsSuccess = result.IsSuccess,
                    Message = result.Message,
                    StatusCode = result.StatusCode,
                    Links = []
                });
        }

        [HttpGet("DemoteUserRole")]
        public async Task<IActionResult> DemoteUserRole([FromBody] DemoteUserRoleCommandRequest request)
        {
            var result = await _mediator.Send(new DemoteUserRoleCommand(request, User.Claims));

            return AcceptedAtRoute("LogOut",
                new { Area = "User", userId = result.Data },
                new ApiResultDto
                {
                    IsSuccess = result.IsSuccess,
                    Message = result.Message,
                    StatusCode = result.StatusCode,
                    Links = []
                });
        }
    }
}
