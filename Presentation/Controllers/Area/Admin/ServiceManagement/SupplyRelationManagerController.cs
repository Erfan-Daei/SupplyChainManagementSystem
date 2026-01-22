using Application.Services.MediatR.Commands.Admin.ServiceManagement.AddSupplyRelation;
using Application.Services.MediatR.Queries.Admin.ServiceManagement.GetSupplyRelationDetail;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Area.Admin.ServiceManagement;
using Presentation.Output.Base;

namespace Presentation.Controllers.Area.Admin.ServiceManagement
{
    [Area("Admin")]
    [Route("api/[area]/ServiceManagement/[controller]")]
    [ApiController]
    public class SupplyRelationManagerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SupplyRelationManagerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddSupplyRelation([FromBody] AddSupplyRelationCommand request)
        {
            var result = await _mediator.Send(request);

            return Created($"/api/Admin/ServiceManagement/SupplyRelationManager/GetSupplyRelationDetail?supplyRelationId={result.Data}"
                , new ApiResultDto<Guid>
                {
                    Data = result.Data,
                    IsSuccess = result.IsSuccess,
                    Message = result.Message,
                    StatusCode = result.StatusCode,
                    Links = []
                });
        }

        [HttpGet("GetSupplyRelationDetail")]
        public async Task<IActionResult> GetSupplyRelationDetail([FromQuery] GetSupplyRelationDetailQueryRequest request)
        {
            var result = await _mediator.Send(new GetSupplyRelationDetailQuery(request, User.Claims));

            return Ok(new ApiResultDto<ApiGetSupplyRelationDetailDto>
            {
                Data = new ApiGetSupplyRelationDetailDto
                {
                    SupplyRelationIsActive = result.Data!.SupplyRelationIsActive,
                    ServiceId = result.Data!.ServiceId,
                    ServiceName = result.Data!.ServiceName,
                    ServiceDescription = result.Data!.ServiceDescription,
                    SupplierCompanyId = result.Data!.SupplierCompanyId,
                    SupplierCompanyName = result.Data!.SupplierCompanyName,
                    ConsumerCompanyId = result.Data!.ConsumerCompanyId,
                    ConsumerCompanyName = result.Data!.ConsumerCompanyName
                },
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Links = []
            });
        }
    }
}
