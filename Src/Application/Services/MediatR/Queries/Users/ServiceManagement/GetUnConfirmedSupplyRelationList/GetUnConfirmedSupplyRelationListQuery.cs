using Application.Interfaces.Auth;
using Application.Services.Implement.Queries.Users.ServiceManagement.GetUnConfirmedSupplyRelationList;
using Common.Output;
using MediatR;
using System.Net;
using System.Security.Claims;

namespace Application.Services.MediatR.Queries.Users.ServiceManagement.GetUnConfirmedSupplyRelationList
{
    //MediatR Query for GetUnConfirmedSupplyRelationListService
    public record GetUnConfirmedSupplyRelationListQueryRequest(Guid companyId) : IRequest<GetUnConfirmedSupplyRelationListQuery>;
    public record GetUnConfirmedSupplyRelationListQuery(GetUnConfirmedSupplyRelationListQueryRequest QueryRequest, IEnumerable<Claim> userClaims) : IRequest<ResultDto<List<GetUnConfirmedSupplyRelationListResultDto>>>;

    //MediatR QueryHandler for GetUnConfirmedSupplyRelationListService
    public class GetUnConfirmedSupplyRelationListQueryHandler : IRequestHandler<GetUnConfirmedSupplyRelationListQuery, ResultDto<List<GetUnConfirmedSupplyRelationListResultDto>>>
    {
        private readonly IAuthManager _authManager;
        private readonly IGetUnConfirmedSupplyRelationList _getUnConfirmedSupplyRelationList;
        public GetUnConfirmedSupplyRelationListQueryHandler(IAuthManager authManager
            , IGetUnConfirmedSupplyRelationList getUnConfirmedSupplyRelationList)
        {
            _authManager = authManager;
            _getUnConfirmedSupplyRelationList = getUnConfirmedSupplyRelationList;
        }
        public async Task<ResultDto<List<GetUnConfirmedSupplyRelationListResultDto>>> Handle(GetUnConfirmedSupplyRelationListQuery request, CancellationToken cancellationToken)
        {
            var checkAccess = await _authManager.CheckAccessToSupplyRelation(request.userClaims, request.QueryRequest.companyId);
            if (!checkAccess)
                return ResultDto<List<GetUnConfirmedSupplyRelationListResultDto>>.Failed(ResultDtoMessageLibrary.Forbidden, HttpStatusCode.Forbidden);

            return await _getUnConfirmedSupplyRelationList.GetUnConfirmedSupplyRelationListAsync(request.QueryRequest, cancellationToken);
        }
    }
}
