using Application.Interfaces.Auth;
using Application.Interfaces.JWT;
using Application.Services.Implement.Commands.Users.UserManagement.ChangePassword.RequestChangePassword;
using Common.Output;
using MediatR;
using System.Net;
using System.Security.Claims;

namespace Application.Services.MediatR.Commands.User.UserManagement.ChangePassword.RequestChangePassword
{
    //MediatR command for RequestChangePasswordService
    public record RequestChangePasswordCommandRequest(Guid? userId, string Password, string ConPassword) : IRequest<RequestChangePasswordCommand>;
    public record RequestChangePasswordCommand(RequestChangePasswordCommandRequest commandRequest, IEnumerable<Claim> userClaims) : IRequest<ResultDto>;

    //MediatR commandHandler for RequestChangePasswordService
    public class RequestChangePasswordCommandHandler : IRequestHandler<RequestChangePasswordCommand, ResultDto>
    {
        private readonly IAuthManager _authManager;
        private readonly IJwtTokenManager _jwtTokenManager;
        private readonly IRequestChangePassword _requestChangePassword;
        public RequestChangePasswordCommandHandler(IAuthManager authManager
            , IJwtTokenManager jwtTokenManager
            , IRequestChangePassword requestChangePassword)
        {
            _authManager = authManager;
            _jwtTokenManager = jwtTokenManager;
            _requestChangePassword = requestChangePassword;
        }
        public async Task<ResultDto> Handle(RequestChangePasswordCommand request, CancellationToken cancellationToken)
        {
            Guid userId;

            //check if admin wants to delete has access to this user
            if (request.commandRequest.userId != null)
            {
                userId = request.commandRequest.userId ?? Guid.Empty;

                var adminId = _jwtTokenManager.GetUserId(request.userClaims);
                if (userId != adminId)
                {
                    var checkAccess = await _authManager.CheckAccessToUser(request.userClaims, userId);
                    if (!checkAccess)
                        return ResultDto.Failed(ResultDtoMessageLibrary.Forbidden, HttpStatusCode.Forbidden);
                }
            }

            userId = _jwtTokenManager.GetUserId(request.userClaims);

            return await _requestChangePassword.RequestChangePasswordAsync(new RequestChangePasswordCommandRequest(userId, request.commandRequest.Password, request.commandRequest.ConPassword), cancellationToken);
        }
    }
}
