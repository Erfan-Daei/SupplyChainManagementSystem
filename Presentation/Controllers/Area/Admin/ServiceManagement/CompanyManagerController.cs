using Application.Services.MediatR.Commands.Admin.ServiceManagement.AddCompany;
using Application.Services.MediatR.Commands.Admin.ServiceManagement.DeleteCompany;
using Application.Services.MediatR.Commands.Admin.ServiceManagement.EditCompany;
using Application.Services.MediatR.Queries.Admin.ServiceManagement.GetCompanyDetail;
using Application.Services.MediatR.Queries.Admin.ServiceManagement.GetCompanyList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Area.Admin.ServiceManagement;
using Presentation.Output.Base;

namespace Presentation.Controllers.Area.Admin.ServiceManagement
{
    [Area("Admin")]
    [Route("api/[area]/ServiceManagement/[controller]")]
    [ApiController]
    public class CompanyManagerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CompanyManagerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[Authorize("SuperAdminOnly")]
        [HttpPost("AddCompany")]
        public async Task<IActionResult> AddCompany([FromBody] AddCompanyCommand request)
        {
            var result = await _mediator.Send(request);

            return Ok(new ApiResultDto<Guid>
            {
                Data = result.Data,
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Links = []
            });
        }

        //[Authorize]
        [HttpGet("GetCompanyList")]
        public async Task<IActionResult> GetCompanyList()
        {
            var result = await _mediator.Send(new GetCompanyListQuery());

            return Ok(new ApiResultDto<List<ApiGetCompanyListDto>>
            {
                Data = result.Data?.Select(r => new ApiGetCompanyListDto
                {
                    CompanyId = r.CompanyId,
                    CompanyName = r.CompanyName,
                }).ToList() ?? [],
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Links = []
            });
        }

        //[Authorize]
        [HttpGet("GetCompanyDetail")]
        public async Task<IActionResult> GetCompanyDetail([FromQuery] GetCompanyDetailQuery request)
        {
            var result = await _mediator.Send(request);

            return Ok(new ApiResultDto<ApiGetCompanyDetaiDto>
            {
                Data = new ApiGetCompanyDetaiDto
                {
                    CompanyName = result.Data!.CompanyName,
                    UserCount = result.Data!.UserCount,
                    AsSupplierCount = result.Data!.AsSupplierCount,
                    AsConsumerCount = result.Data!.AsConsumerCount,
                    CompanyServices = result.Data.CompanyServices.Select(s => new ApiGetCompanyDetailCompanyServicesDto
                    {
                        ServiceId = s.ServiceId,
                        ServiceName = s.ServiceName,
                    }).ToList() ?? []
                },
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Links = []
            });
        }

        //[Authorize("AdminsOnly")]
        [HttpPut("EditCompany")]
        public async Task<IActionResult> EditCompany([FromBody] EditCompanyCommandRequest request)
        {
            var result = await _mediator.Send(new EditCompanyCommand(request, User.Claims));

            return Ok(new ApiResultDto
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Links = []
            });
        }

        //[Authorize("SuperAdminOnly")]
        [HttpDelete("DeleteCompany")]
        public async Task<IActionResult> DeleteCompany([FromBody] DeleteCompanyCommand request)
        {
            var result = await _mediator.Send(request);

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
