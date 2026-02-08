using Application.Interfaces.Auth;
using Application.Interfaces.JWT;
using Application.Services.Implement.Commands.Users.UserManagement.ChangePassword.VerifyChangePassword;
using Common.Output;
using MediatR;
using System.Net;
using System.Security.Claims;

namespace Application.Services.MediatR.Commands.User.UserManagement.ChangePassword.VerifyChangePassword
{
    //MediatR command for VerifyChangePasswordService
    public record VerifyChangePasswordCommandRequest(Guid? userId, string token) : IRequest<VerifyChangePasswordCommand>;
    public record VerifyChangePasswordCommand(VerifyChangePasswordCommandRequest commandRequest, IEnumerable<Claim> userClaims) : IRequest<ResultDto>;

    //MediatR commandHandler for VerifyChangePasswordService
    public class VerifyChangePasswordCommandHandler : IRequestHandler<VerifyChangePasswordCommand, ResultDto>
    {
        private readonly IAuthManager _authManager;
        private readonly IJwtTokenManager _jwtTokenManager;
        private readonly IVerifyChangePassword _verifyChangePassword;
        public VerifyChangePasswordCommandHandler(IAuthManager authManager
            , IJwtTokenManager jwtTokenManager
            , IVerifyChangePassword verifyChangePassword)
        {
            _authManager = authManager;
            _jwtTokenManager = jwtTokenManager;
            _verifyChangePassword = verifyChangePassword;
        }
        public async Task<ResultDto> Handle(VerifyChangePasswordCommand request, CancellationToken cancellationToken)
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

            return await _verifyChangePassword.VerifyChangePasswordAsync(new VerifyChangePasswordCommandRequest(userId, request.commandRequest.token), cancellationToken);
        }
    }
}
