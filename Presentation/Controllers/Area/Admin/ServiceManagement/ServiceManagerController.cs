using Application.Services.MediatR.Commands.Admin.ServiceManagement.AddService;
using Application.Services.MediatR.Commands.Admin.ServiceManagement.ConfirmService;
using Application.Services.MediatR.Commands.Admin.ServiceManagement.DeleteService;
using Application.Services.MediatR.Commands.Admin.ServiceManagement.EditService;
using Application.Services.MediatR.Queries.Users.ServiceManagement.GetServiceDetail;
using Application.Services.MediatR.Queries.Users.ServiceManagement.GetServiceList;
using Common.Output;
using Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Area.Admin.ServiceManagement.Service;
using Presentation.Output.Base;

namespace Presentation.Controllers.Area.Admin.ServiceManagement
{
    [Area("Admin")]
    [Route("api/[area]/ServiceManagement/[controller]")]
    [Authorize]
    [ApiController]
    public class ServiceManagerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ServiceManagerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(AuthPolicy.AdminsOnlyName)]
        [HttpPost("AddService")]
        public async Task<IActionResult> AddService([FromBody] AddServiceCommandRequest request)
        {
            var result = await _mediator.Send(new AddServiceCommand(request, User.Claims));

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiAddServiceResult.Result(result.Data, Url)
            });
        }

        [Area("User")]
        [AllowAnonymous]
        [HttpGet("GetServiceDetail")]
        public async Task<IActionResult> GetServiceDetail([FromQuery] GetServiceDetailQuery request)
        {
            var result = await _mediator.Send(request);

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiGetServiceDetailResult.Result(request.serviceId, result.Data!, Url)
            });
        }

        [Area("User")]
        [AllowAnonymous]
        [HttpGet("GetServiceList")]
        public async Task<IActionResult> GetServiceList()
        {
            var result = await _mediator.Send(new GetServiceListQuery());

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiGetServiceListResult.Result(result.Data!, Url)
            });
        }

        [Authorize(AuthPolicy.AdminsOnlyName)]
        [HttpPut("EditService")]
        public async Task<IActionResult> EditService([FromBody] EditServiceCommandRequest request)
        {
            var result = await _mediator.Send(new EditServiceCommand(request, User.Claims));

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiEditServiecResult.Result(request.Dto.ServiceId, Url)
            });
        }

        [Authorize(AuthPolicy.SuperAdminsOnlyName)]
        [HttpDelete("DeleteService")]
        public async Task<IActionResult> DeleteService([FromQuery] DeleteServiceCommandRequest request)
        {
            var result = await _mediator.Send(new DeleteServiceCommand(request, User.Claims));

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = null
            });
        }

        [Authorize(AuthPolicy.SuperAdminsOnlyName)]
        [HttpPut("ConfirmService")]
        public async Task<IActionResult> ConfirmService([FromQuery] ConfirmServiceCommand request)
        {
            var result = await _mediator.Send(request);

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiConfirmServiecResult.Result(request.serviceId, Url)
            });
        }
    }
}
