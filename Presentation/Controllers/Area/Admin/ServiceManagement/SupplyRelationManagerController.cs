using Application.Services.MediatR.Commands.Admin.ServiceManagement.AddSupplyRelation;
using Application.Services.MediatR.Commands.Admin.ServiceManagement.ConfirmSupplyRelation;
using Application.Services.MediatR.Queries.Admin.ServiceManagement.GetSupplyRelationAsConsumer;
using Application.Services.MediatR.Queries.Admin.ServiceManagement.GetSupplyRelationDetail;
using Application.Services.MediatR.Queries.Admin.ServiceManagement.GetSupplyRelationListAsSupplier;
using Application.Services.MediatR.Queries.Users.ServiceManagement.GetUnConfirmedSupplyRelationList;
using Common.Output;
using Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Area.Admin.ServiceManagement.SupplyRelation;
using Presentation.Output.Base;

namespace Presentation.Controllers.Area.Admin.ServiceManagement
{
    [Area("Admin")]
    [Route("api/[area]/ServiceManagement/[controller]")]
    [Authorize]
    [ApiController]
    public class SupplyRelationManagerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SupplyRelationManagerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(AuthPolicy.AdminsOnlyName)]
        [HttpPost("AddSupplyRelation")]
        public async Task<IActionResult> AddSupplyRelation([FromBody] AddSupplyRelationCommandRequest request)
        {
            var result = await _mediator.Send(new AddSupplyRelationCommand(request, User.Claims));

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiAddSupplyRelationResult.Result(result.Data, Url)
            });
        }

        [Authorize(AuthPolicy.UserOrHigherName)]
        [HttpGet("GetSupplyRelationDetail")]
        public async Task<IActionResult> GetSupplyRelationDetail([FromQuery] GetSupplyRelationDetailQueryRequest request)
        {
            var result = await _mediator.Send(new GetSupplyRelationDetailQuery(request, User.Claims));

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiGetSupplyRelationDetailResult.Result(request.supplyRelationId, result.Data!, Url)
            });
        }

        [Authorize(AuthPolicy.AdminsOnlyName)]
        [HttpPut("ConfirmSupplyRelation")]
        public async Task<IActionResult> ConfirmSupplyRelation([FromQuery] ConfirmSupplyRelationCommandRequest request)
        {
            var result = await _mediator.Send(new ConfirmSupplyRelationCommand(request, User.Claims));

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiConfirmSupplyRelationResult.Result(request.supplyRelationId, Url)
            });
        }

        [Authorize(AuthPolicy.UserOrHigherName)]
        [HttpGet("GetSupplyRelationAsSupplier")]
        public async Task<IActionResult> GetSupplyRelationAsSupplier([FromQuery] GetSupplyRelationAsSupplierQueryRequest request)
        {
            var result = await _mediator.Send(new GetSupplyRelationAsSupplierQuery(request, User.Claims));

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiGetSupplyRelationAsSupplierResult.Result(request.companyId, result.Data!, Url)
            });
        }

        [Authorize(AuthPolicy.UserOrHigherName)]
        [HttpGet("GetSupplyRelationAsConsumer")]
        public async Task<IActionResult> GetSupplyRelationAsConsumer([FromQuery] GetSupplyRelationAsConsumerQueryRequest request)
        {
            var result = await _mediator.Send(new GetSupplyRelationAsConsumerQuery(request, User.Claims));

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiGetSupplyRelationAsConsumerResult.Result(request.companyId, result.Data!, Url)
            });
        }

        [Authorize(AuthPolicy.UserOrHigherName)]
        [HttpGet("GetUnConfirmedSupplyRelationList")]
        public async Task<IActionResult> GetUnConfirmedSupplyRelationList([FromQuery] GetUnConfirmedSupplyRelationListQueryRequest request)
        {
            var result = await _mediator.Send(new GetUnConfirmedSupplyRelationListQuery(request, User.Claims));

            return this.ApiResult(new ResultDto<object>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                StatusCode = result.StatusCode,
                Data = ApiGetUnConfirmedSupplyRelationListResult.Result(result.Data!, Url)
            });
        }
    }
}
