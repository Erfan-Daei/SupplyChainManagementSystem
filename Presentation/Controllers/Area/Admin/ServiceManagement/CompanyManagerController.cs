using Application.Services.MediatR.Commands.Admin.ServiceManagement.AddCompany;
using Application.Services.MediatR.Commands.Admin.ServiceManagement.AssignServiceToCompany;
using Application.Services.MediatR.Commands.Admin.ServiceManagement.DeleteCompany;
using Application.Services.MediatR.Commands.Admin.ServiceManagement.EditCompany;
using Application.Services.MediatR.Commands.Admin.ServiceManagement.UnAssignServiceFromCompany;
using Application.Services.MediatR.Queries.Admin.ServiceManagement.GetCompanyDetail;
using Application.Services.MediatR.Queries.Admin.ServiceManagement.GetCompanyList;
using Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Area.Admin.ServiceManagement;
using Presentation.Output.Base;

namespace Presentation.Controllers.Area.Admin.ServiceManagement
{
    [Area("Admin")]
    [Route("api/[area]/ServiceManagement/[controller]")]
    [Authorize]
    [ApiController]
    public class CompanyManagerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CompanyManagerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(AuthPolicy.SuperAdminsOnlyName)]
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

        [AllowAnonymous]
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

        [AllowAnonymous]
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

        [Authorize(AuthPolicy.AdminsOnlyName)]
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

        [Authorize(AuthPolicy.SuperAdminsOnlyName)]
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

        [Authorize(AuthPolicy.UserOrHigherName)]
        [HttpPost("AssignServiceToCompany")]
        public async Task<IActionResult> AssignServiceToCompany([FromBody] AssignServiceToCompanyCommandRequest request)
        {
            var result = await _mediator.Send(new AssignServiceToCompanyCommand(request, User.Claims));

            return Ok(new ApiResultDto
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Links = []
            });
        }

        [Authorize(AuthPolicy.UserOrHigherName)]
        [HttpPost("UnAssignServiceFromCompany")]
        public async Task<IActionResult> UnAssignServiceToCompany([FromBody] UnAssignServiceFromCompanyCommandRequest request)
        {
            var result = await _mediator.Send(new UnAssignServiceFromCompanyCommand(request, User.Claims));

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
