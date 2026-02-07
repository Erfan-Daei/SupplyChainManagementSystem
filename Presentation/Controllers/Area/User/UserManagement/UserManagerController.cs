using Application.Services.MediatR.Commands.Admin.UserManagement.AssignCompanyToUser;
using Application.Services.MediatR.Commands.User.UserManagement.GetUserDetail;
using Application.Services.MediatR.Queries.Users.UserManagement.GetUserList;
using Common.Output;
using Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Area.User.UserManagement.Users;
using Presentation.Output.Base;

namespace Presentation.Controllers.Area.User.UserManagement
{
    [Area("User")]
    [Route("api/[area]/UserManagement/[controller]")]
    [Authorize]
    [ApiController]
    public class UserManagerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserManagerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(AuthPolicy.AdminsOnlyName)]
        [HttpPut("AssignCompanyToUser")]
        public async Task<IActionResult> AssignCompanyToUser([FromBody] AssignCompanyToUserCommandRequest request)
        {
            var result = await _mediator.Send(new AssignCompanyToUserCommand(request, User.Claims));

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiAssignCompanyToUserResult.Result(request.userId, request.companyId, Url)
            });
        }

        [HttpGet("GetUserDetail")]
        public async Task<IActionResult> GetUserDetail([FromQuery] GetUserDetailQueryRequest request)
        {
            var result = await _mediator.Send(new GetUserDetailQuery(request, User.Claims));

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiGetUserDetailResult.Result(request.userId ?? Guid.Empty, result.Data!, Url)
            });
        }

        [Authorize(AuthPolicy.AdminsOnlyName)]
        [HttpGet("GetUserList")]
        public async Task<IActionResult> GetUserList([FromQuery] GetUserListQueryRequest request)
        {
            var result = await _mediator.Send(new GetUserListQuery(request, User.Claims));

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiGetUserListResult.Result(result.Data!, Url)
            });
        }
    }
}
