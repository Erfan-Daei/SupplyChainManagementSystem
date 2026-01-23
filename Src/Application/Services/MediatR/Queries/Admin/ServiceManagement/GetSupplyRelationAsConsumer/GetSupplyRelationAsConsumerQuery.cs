using Application.Interfaces.Auth;
using Application.Services.Implement.Queries.Admin.ServiceManagement.GetSupplyRelationAsConsumer;
using Common.Output;
using MediatR;
using System.Security.Claims;

namespace Application.Services.MediatR.Queries.Admin.ServiceManagement.GetSupplyRelationAsConsumer
{
    //MediatR query for GetSupplyRelationAsConsumerService
    public record GetSupplyRelationAsConsumerQueryRequest(Guid companyId) : IRequest<GetSupplyRelationAsConsumerQuery>;
    public record GetSupplyRelationAsConsumerQuery(GetSupplyRelationAsConsumerQueryRequest queryRequest, IEnumerable<Claim> adminClaims) : IRequest<ResultDto<List<GetSupplyRelationAsConsumerResultDto>>>;

    //MediatR queryHandler for GetSupplyRelationAsConsumerService\
    public class GetSupplyRelationAsConsumerQueryHandler : IRequestHandler<GetSupplyRelationAsConsumerQuery, ResultDto<List<GetSupplyRelationAsConsumerResultDto>>>
    {
        private readonly IAuthManager _authManager;
        private readonly IGetSupplyRelationAsConsumer _getSupplyRelationAsConsumer;
        public GetSupplyRelationAsConsumerQueryHandler(IAuthManager authManager
            , IGetSupplyRelationAsConsumer getSupplyRelationAsConsumer)
        {
            _authManager = authManager;
            _getSupplyRelationAsConsumer = getSupplyRelationAsConsumer;
        }
        public async Task<ResultDto<List<GetSupplyRelationAsConsumerResultDto>>> Handle(GetSupplyRelationAsConsumerQuery request, CancellationToken cancellationToken)
        {
            /*var checkAccess = await _authManager.CheckAccessToCompany(request.adminClaims, request.queryRequest.companyId);
            if (!checkAccess)
                return ResultDto<List<GetSupplyRelationAsConsumerResultDto>>.Failed(ResultDtoMessageLibrary.UnAuthorized, HttpStatusCode.Unauthorized);*/

            return await _getSupplyRelationAsConsumer.GetSupplyRelationAsConsumerAsync(request.queryRequest, cancellationToken);
        }
    }
}
