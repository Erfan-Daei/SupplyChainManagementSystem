using Application.Services.MediatR.Commands.Admin.UserManagement.DemoteUserRole;
using Application.Services.MediatR.Commands.Admin.UserManagement.PromoteUserRole;
using Common.Output;
using Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Area.User.UserManagement.Role;
using Presentation.Output.Base;

namespace Presentation.Controllers.Area.User.UserManagement
{
    [Area("Admin")]
    [Route("api/[area]/UserManagement/[controller]")]
    [Authorize]
    [ApiController]
    public class RoleManagerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RoleManagerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(AuthPolicy.AdminsOnlyName)]
        [HttpPost("PromoteUserRole")]
        public async Task<IActionResult> PromoteUserRole([FromBody] PromoteUserRoleCommandRequest request)
        {
            var result = await _mediator.Send(new PromoteUserRoleCommand(request, User.Claims));

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiChangeUserRoleResult.Result(request.userId, Url)
            });
        }

        [Authorize(AuthPolicy.AdminsOnlyName)]
        [HttpPost("DemoteUserRole")]
        public async Task<IActionResult> DemoteUserRole([FromBody] DemoteUserRoleCommandRequest request)
        {
            var result = await _mediator.Send(new DemoteUserRoleCommand(request, User.Claims));

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiChangeUserRoleResult.Result(request.userId, Url)
            });
        }
    }
}
