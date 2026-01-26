using Application.Services.MediatR.Commands.Admin.UserManagement.AssignCompanyToUser;
using Application.Services.MediatR.Commands.User.UserManagement.GetUserDetail;
using Application.Services.MediatR.Commands.User.UserManagement.GetUserList;
using Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Area.User.UserManagement;
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

            return Ok(new ApiResultDto
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Links = []
            });
        }

        [HttpGet("GetUserDetail")]
        public async Task<IActionResult> GetUserDetail([FromBody] GetUserDetailQueryRequest request)
        {
            var result = await _mediator.Send(new GetUserDetailQuery(request, User.Claims));

            return Ok(new ApiResultDto<ApiGetUserDetailDto>
            {
                Data = new ApiGetUserDetailDto
                {
                    UserFullName = result.Data!.UserFullName,
                    UserEmail = result.Data!.UserEmail,
                    UserCompanyName = result.Data!.UserCompanyName,
                    UserCompanyId = result.Data!.UserCompanyId,
                    UserRole = result.Data!.UserRole,
                    CreatedAt = result.Data!.CreatedAt,
                },
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Links = []
            });
        }

        [Authorize(AuthPolicy.AdminsOnlyName)]
        [HttpGet("GetUserList")]
        public async Task<IActionResult> GetUserList([FromQuery] GetUserListQueryRequest request)
        {
            var result = await _mediator.Send(new GetUserListQuery(request, User.Claims));

            return Ok(new ApiResultDto<List<ApiGetUserListDto>>
            {
                Data = result.Data?.Select(u => new ApiGetUserListDto
                {
                    UserId = u.UserId,
                    UserFullName = u.UserFullName,
                    UserEmail = u.UserEmail,
                }).ToList() ?? [],
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Links = []
            });
        }
    }
}
