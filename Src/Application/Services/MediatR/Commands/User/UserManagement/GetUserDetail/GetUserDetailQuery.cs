using Application.Interfaces.Authorization;
using Application.Interfaces.JWT;
using Application.Services.Implement.Queries.Users.UserManagement;
using Common.Output;
using MediatR;
using System.Net;
using System.Security.Claims;

namespace Application.Services.MediatR.Commands.User.UserManagement.GetUserDetail
{
    //MediatR query for GetUserDetailService
    public record GetUserDetailQueryRequest(Guid? userId) : IRequest<GetUserDetailQuery>;
    public record GetUserDetailQuery(GetUserDetailQueryRequest queryRequest, IEnumerable<Claim> userClaims) : IRequest<ResultDto<GetUserDetailResultDto>>;

    //MediatR queryHandler for GetUserDetailService
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, ResultDto<GetUserDetailResultDto>>
    {
        private readonly IAuthManager _authManager;
        private readonly IJwtTokenManager _jwtTokenManager;
        private readonly IGetUserDetail _getUserDetail;
        public GetUserDetailQueryHandler(IAuthManager authManager
            , IJwtTokenManager jwtTokenManager
            , IGetUserDetail getUserDetail)
        {
            _authManager = authManager;
            _jwtTokenManager = jwtTokenManager;
            _getUserDetail = getUserDetail;
        }
        public async Task<ResultDto<GetUserDetailResultDto>> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            if (request.queryRequest.userId == null || request.queryRequest.userId == Guid.Empty)
            {
                var userId = _jwtTokenManager.GetUserId(request.userClaims);
                return await _getUserDetail.GetUserDetailAsync(new GetUserDetailQueryRequest(userId), cancellationToken);
            }
            else
            {
                var checkAccess = await _authManager.CheckAccessToUser(request.userClaims, request.queryRequest.userId ?? Guid.Empty);
                if (!checkAccess)
                    return ResultDto<GetUserDetailResultDto>.Failed(ResultDtoMessageLibrary.UnAuthorized, HttpStatusCode.Unauthorized);

                return await _getUserDetail.GetUserDetailAsync(request.queryRequest, cancellationToken);
            }
        }
    }
}
