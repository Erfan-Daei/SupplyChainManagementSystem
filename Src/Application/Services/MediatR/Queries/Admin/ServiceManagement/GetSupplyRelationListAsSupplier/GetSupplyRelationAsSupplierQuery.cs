using Application.Interfaces.Auth;
using Application.Services.Implement.Queries.Admin.ServiceManagement.GetSupplyRelationListAsSupplier;
using Common.Output;
using MediatR;
using System.Security.Claims;

namespace Application.Services.MediatR.Queries.Admin.ServiceManagement.GetSupplyRelationListAsSupplier
{
    //MediatR query for GetSupplyRelationAsSupplierService
    public record GetSupplyRelationAsSupplierQueryRequest(Guid companyId) : IRequest<GetSupplyRelationAsSupplierQuery>;
    public record GetSupplyRelationAsSupplierQuery(GetSupplyRelationAsSupplierQueryRequest queryRequest, IEnumerable<Claim> adminClaims) : IRequest<ResultDto<List<GetSupplyRelationAsSupplierResultDto>>>;

    //MediatR queryHandler for GetSupplyRelationAsSupplierService
    public class GetSupplyRelationAsSupplierQueryHandler : IRequestHandler<GetSupplyRelationAsSupplierQuery, ResultDto<List<GetSupplyRelationAsSupplierResultDto>>>
    {
        private readonly IAuthManager _authManager;
        private readonly IGetSupplyRelationAsSupplier _getSupplyRelationAsSupplier;
        public GetSupplyRelationAsSupplierQueryHandler(IAuthManager authManager
            , IGetSupplyRelationAsSupplier getSupplyRelationAsSupplier)
        {
            _authManager = authManager;
            _getSupplyRelationAsSupplier = getSupplyRelationAsSupplier;
        }
        public async Task<ResultDto<List<GetSupplyRelationAsSupplierResultDto>>> Handle(GetSupplyRelationAsSupplierQuery request, CancellationToken cancellationToken)
        {
            /*var checkAccess = await _authManager.CheckAccessToCompany(request.adminClaims, request.queryRequest.companyId);
            if (!checkAccess)
                return ResultDto<List<GetSupplyRelationAsSupplierResultDto>>.Failed(ResultDtoMessageLibrary.UnAuthorized, HttpStatusCode.Unauthorized);*/

            return await _getSupplyRelationAsSupplier.GetSupplyRelationAsSupplierAsync(request.queryRequest, cancellationToken);
        }
    }
}
