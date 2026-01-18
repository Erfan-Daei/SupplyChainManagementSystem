using Application.Services.MediatR.Commands.Admin.ServiceManagement.AddService;
using Application.Services.MediatR.Commands.Admin.ServiceManagement.DeleteService;
using Application.Services.MediatR.Commands.Admin.ServiceManagement.EditService;
using Application.Services.MediatR.Queries.Users.ServiceManagement.GetServiceDetail;
using Application.Services.MediatR.Queries.Users.ServiceManagement.GetServiceList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Area.User.ServiceManagement;
using Presentation.Output.Base;

namespace Presentation.Controllers.Area.Admin.ServiceManagement
{
    [Area("Admin")]
    [Route("api/[area]/ServiceManagement/[controller]")]
    [ApiController]
    public class ServiceManagerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ServiceManagerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddService")]
        public async Task<IActionResult> AddService([FromBody] AddServiceCommandRequest request)
        {
            var result = await _mediator.Send(new AddServiceCommand(request, User.Claims));

            return Created($"/api/Admin/ServiceManagement/ServiceManager/GetServiceDetail?ServiceId={result.Data}",
                new ApiResultDto<Guid>
                {
                    Data = result.Data,
                    IsSuccess = result.IsSuccess,
                    Message = result.Message,
                    StatusCode = result.StatusCode,
                    Links = []
                });
        }

        [Area("User")]
        [HttpGet("GetServiceDetail")]
        public async Task<IActionResult> GetServiceDetail([FromQuery] GetServiceDetailQuery request)
        {
            var result = await _mediator.Send(request);

            return Ok(new ApiResultDto<ApiGetServiceDetailDro>
            {
                Data = new ApiGetServiceDetailDro
                {
                    Creator = result.Data!.Creator,
                    CreatedAt = result.Data.CreatedAt,
                    ServiceDescription = result.Data.ServiceDescription,
                    ServiceIsActive = result.Data.ServiceIsActive,
                    ServiceName = result.Data.ServiceName,
                },
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Links = []
            });
        }

        [Area("User")]
        [HttpGet("GetServiceList")]
        public async Task<IActionResult> GetServiceList()
        {
            var result = await _mediator.Send(new GetServiceListQuery());

            var mappedResult = result.Data?.Select(r => new ApiGetServiceListDto
            {
                ServiceId = r.ServiceId,
                ServiceName = r.ServiceName,
                ServiceIsActive = r.ServiceIsActive,
            }).ToList() ?? [];

            return Ok(new ApiResultDto<List<ApiGetServiceListDto>>
            {
                Data = mappedResult,
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Links = []
            });
        }

        [HttpPut("EditService")]
        public async Task<IActionResult> EditService([FromBody] EditServiceCommandRequest request)
        {
            var result = await _mediator.Send(new EditServiceCommand(request, User.Claims));

            return Ok(new ApiResultDto
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Links = []
            });
        }

        [HttpDelete("DeleteService")]
        public async Task<IActionResult> DeleteService([FromQuery] DeleteServiceCommandRequest request)
        {
            var result = await _mediator.Send(new DeleteServiceCommand(request, User.Claims));

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
