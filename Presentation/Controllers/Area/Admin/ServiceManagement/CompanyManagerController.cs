using Application.Services.MediatR.Commands.Admin.ServiceManagement.AddCompany;
using Application.Services.MediatR.Commands.Admin.ServiceManagement.AssignServiceToCompany;
using Application.Services.MediatR.Commands.Admin.ServiceManagement.DeleteCompany;
using Application.Services.MediatR.Commands.Admin.ServiceManagement.EditCompany;
using Application.Services.MediatR.Commands.Admin.ServiceManagement.UnAssignServiceFromCompany;
using Application.Services.MediatR.Queries.Admin.ServiceManagement.GetCompanyDetail;
using Application.Services.MediatR.Queries.Admin.ServiceManagement.GetCompanyList;
using Common.Output;
using Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Area.Admin.ServiceManagement.Company;
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

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiAddCompanyResult.Result(result.Data, Url)
            });
        }

        [AllowAnonymous]
        [HttpGet("GetCompanyList")]
        public async Task<IActionResult> GetCompanyList()
        {
            var result = await _mediator.Send(new GetCompanyListQuery());

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiGetCompanyListResult.Result(result.Data!, Url)
            });
        }

        [AllowAnonymous]
        [HttpGet("GetCompanyDetail")]
        public async Task<IActionResult> GetCompanyDetail([FromQuery] GetCompanyDetailQuery request)
        {
            var result = await _mediator.Send(request);

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiGetCompanyDetaiResult.Result(request.companyId, result.Data!, Url)
            });
        }

        [Authorize(AuthPolicy.AdminsOnlyName)]
        [HttpPut("EditCompany")]
        public async Task<IActionResult> EditCompany([FromBody] EditCompanyCommandRequest request)
        {
            var result = await _mediator.Send(new EditCompanyCommand(request, User.Claims));

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiEditCompanyResult.Result(request.companyId, Url)
            });
        }

        [Authorize(AuthPolicy.SuperAdminsOnlyName)]
        [HttpDelete("DeleteCompany")]
        public async Task<IActionResult> DeleteCompany([FromBody] DeleteCompanyCommand request)
        {
            var result = await _mediator.Send(request);

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = null
            });
        }

        [Authorize(AuthPolicy.UserOrHigherName)]
        [HttpPost("AssignServiceToCompany")]
        public async Task<IActionResult> AssignServiceToCompany([FromBody] AssignServiceToCompanyCommandRequest request)
        {
            var result = await _mediator.Send(new AssignServiceToCompanyCommand(request, User.Claims));

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiAssignmentServiceToCompany.Result(request.companyId, request.serviceId, Url)
            });
        }

        [Authorize(AuthPolicy.UserOrHigherName)]
        [HttpPost("UnAssignServiceFromCompany")]
        public async Task<IActionResult> UnAssignServiceToCompany([FromBody] UnAssignServiceFromCompanyCommandRequest request)
        {
            var result = await _mediator.Send(new UnAssignServiceFromCompanyCommand(request, User.Claims));

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiAssignmentServiceToCompany.Result(request.companyId, request.serviceId, Url)
            });
        }
    }
}
