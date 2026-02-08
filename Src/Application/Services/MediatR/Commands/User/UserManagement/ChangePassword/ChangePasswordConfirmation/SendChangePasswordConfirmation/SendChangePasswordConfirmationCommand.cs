using Application.Interfaces.Auth;
using Application.Interfaces.JWT;
using Application.Services.Implement.Commands.Users.UserManagement.ChangePassword.ChangePasswordConfirmation.SendChangePasswordConfirmation;
using Common.Output;
using MediatR;
using System.Net;
using System.Security.Claims;

namespace Application.Services.MediatR.Commands.User.UserManagement.ChangePassword.ChangePasswordConfirmation.SendChangePasswordConfirmation
{
    //MediatR command for SendChangePasswordConfirmationService
    public record SendChangePasswordConfirmationCommandRequest(Guid? userId) : IRequest<SendChangePasswordConfirmationCommand>;
    public record SendChangePasswordConfirmationCommand(SendChangePasswordConfirmationCommandRequest commandRequest, IEnumerable<Claim> userClaims) : IRequest<ResultDto>;

    //MediatR commandHandler for SendChangePasswordConfirmationService
    public class SendChangePasswordConfirmationCommandHandler : IRequestHandler<SendChangePasswordConfirmationCommand, ResultDto>
    {
        private readonly IAuthManager _authManager;
        private readonly IJwtTokenManager _jwtTokenManager;
        private readonly ISendChangePasswordConfirmation _sendChangePasswordConfirmation;
        public SendChangePasswordConfirmationCommandHandler(IAuthManager authManager
            , IJwtTokenManager jwtTokenManager
            , ISendChangePasswordConfirmation sendChangePasswordConfirmation)
        {
            _authManager = authManager;
            _jwtTokenManager = jwtTokenManager;
            _sendChangePasswordConfirmation = sendChangePasswordConfirmation;
        }
        public async Task<ResultDto> Handle(SendChangePasswordConfirmationCommand request, CancellationToken cancellationToken)
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

            return await _sendChangePasswordConfirmation.SendChangePasswordConfirmationAsync(new SendChangePasswordConfirmationCommandRequest(userId), cancellationToken);
        }
    }
}
