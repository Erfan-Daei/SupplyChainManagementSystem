using Application.Interfaces.Auth;
using Application.Services.Implement.Queries.Users.UserManagement.GetUserList;
using Common.Output;
using MediatR;
using System.Net;
using System.Security.Claims;

namespace Application.Services.MediatR.Commands.User.UserManagement.GetUserList
{
    //MediatR query for GetUserListService
    public record GetUserListQueryRequest(Guid usersCompanyId) : IRequest<GetUserListQuery>;
    public record GetUserListQuery(GetUserListQueryRequest queryRequest, IEnumerable<Claim> adminClaims) : IRequest<ResultDto<List<GetUserListResultDto>>>;

    //MediatR queryHandler for GetUserListService
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, ResultDto<List<GetUserListResultDto>>>
    {
        private readonly IAuthManager _authManager;
        private readonly IGetUserList _getUserList;
        public GetUserListQueryHandler(IAuthManager authManager
            , IGetUserList getUserList)
        {
            _authManager = authManager;
            _getUserList = getUserList;
        }
        public async Task<ResultDto<List<GetUserListResultDto>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var checkAccess = await _authManager.CheckAccessToCompany(request.adminClaims, request.queryRequest.usersCompanyId);
            if (!checkAccess)
                return ResultDto<List<GetUserListResultDto>>.Failed(ResultDtoMessageLibrary.Forbidden, HttpStatusCode.Forbidden);

            return await _getUserList.GetUserListAsync(request.queryRequest, cancellationToken);
        }
    }
}
