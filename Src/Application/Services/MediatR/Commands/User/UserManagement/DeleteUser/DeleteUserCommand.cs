using Application.Interfaces.Auth;
using Application.Interfaces.JWT;
using Application.Services.Implement.Commands.Users.UserManagement.DeleteUser;
using Common.Output;
using MediatR;
using System.Net;
using System.Security.Claims;

namespace Application.Services.MediatR.Commands.User.UserManagement.DeleteUser
{
    //MediatR command for DeleteUserService
    public record DeleteUserCommandRequest(Guid? userId) : IRequest<DeleteUserCommand>;
    public record DeleteUserCommand(DeleteUserCommandRequest commandRequest, IEnumerable<Claim> adminClaims) : IRequest<ResultDto>;

    //MediatR commandHandler for DeleteUserService
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ResultDto>
    {
        private readonly IAuthManager _authManager;
        private readonly IJwtTokenManager _jwtTokenManager;
        private readonly IDeleteUser _deleteUser;
        public DeleteUserCommandHandler(IAuthManager authManager
            , IJwtTokenManager jwtTokenManager
            , IDeleteUser deleteUser)
        {
            _authManager = authManager;
            _jwtTokenManager = jwtTokenManager;
            _deleteUser = deleteUser;
        }
        public async Task<ResultDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            Guid userId;

            //check if admin wants to delete has access to this user
            if (request.commandRequest.userId != null)
            {
                userId = request.commandRequest.userId ?? Guid.Empty;

                var adminId = _jwtTokenManager.GetUserId(request.adminClaims);
                if (userId != adminId)
                {
                    var checkAccess = await _authManager.CheckAccessToUser(request.adminClaims, userId);
                    if (!checkAccess)
                        return ResultDto.Failed(ResultDtoMessageLibrary.Forbidden, HttpStatusCode.Forbidden);
                }
            }

            userId = _jwtTokenManager.GetUserId(request.adminClaims);

            return await _deleteUser.DeleteUserAsync(new DeleteUserCommandRequest(userId), cancellationToken);
        }
    }
}
