using Application.Interfaces.Auth;
using Application.Services.Implement.Queries.Admin.ServiceManagement.GetSupplyRelationDetail;
using Common.Output;
using MediatR;
using System.Net;
using System.Security.Claims;

namespace Application.Services.MediatR.Queries.Admin.ServiceManagement.GetSupplyRelationDetail
{
    //MedaitR query for GetSupplyRelationDetailService
    public record GetSupplyRelationDetailQueryRequest(Guid supplyRelationId) : IRequest<GetSupplyRelationDetailQuery>;
    public record GetSupplyRelationDetailQuery(GetSupplyRelationDetailQueryRequest queryRequest, IEnumerable<Claim> adminClaims) : IRequest<ResultDto<GetSupplyRelationDetailResultDto>>;

    //MedaitR queryHandler for GetSupplyRelationDetailService
    public class GetSupplyRelationDetailQueryHandler : IRequestHandler<GetSupplyRelationDetailQuery, ResultDto<GetSupplyRelationDetailResultDto>>
    {
        private readonly IAuthManager _authManager;
        private readonly IGetSupplyRelationDetail _getSupplyRelationDetail;
        public GetSupplyRelationDetailQueryHandler(IAuthManager authManager
            , IGetSupplyRelationDetail getSupplyRelationDetail)
        {
            _authManager = authManager;
            _getSupplyRelationDetail = getSupplyRelationDetail;
        }
        public async Task<ResultDto<GetSupplyRelationDetailResultDto>> Handle(GetSupplyRelationDetailQuery request, CancellationToken cancellationToken)
        {
            var checkAccess = await _authManager.CheckAccessToSupplyRelation(request.adminClaims, request.queryRequest.supplyRelationId);
            if (!checkAccess)
                return ResultDto<GetSupplyRelationDetailResultDto>.Failed(ResultDtoMessageLibrary.Forbidden, HttpStatusCode.Unauthorized);

            return await _getSupplyRelationDetail.GetSupplyRelationDetailAsync(request.queryRequest, cancellationToken);
        }
    }
}
