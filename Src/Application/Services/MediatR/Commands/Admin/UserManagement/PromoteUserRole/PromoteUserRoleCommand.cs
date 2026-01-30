using Application.Interfaces.Auth;
using Application.Services.Implement.Commands.Admin.UserManagement.PromoteUserRole;
using Common.Output;
using MediatR;
using System.Net;
using System.Security.Claims;

namespace Application.Services.MediatR.Commands.Admin.UserManagement.PromoteUserRole
{
    //MediatR Command for ChangeUserRoleService
    public record PromoteUserRoleCommand(PromoteUserRoleCommandRequest commandRequest, IEnumerable<Claim> claims) : IRequest<ResultDto<Guid>>;
    public record PromoteUserRoleCommandRequest(Guid userId) : IRequest<PromoteUserRoleCommand>;

    //MediatR CommandHandler for ChangeUserRoleService
    public class ChangeUserRoleCommandHandler : IRequestHandler<PromoteUserRoleCommand, ResultDto<Guid>>
    {
        private readonly IPromoteUserRole _changeUserRole;
        private readonly IAuthManager _authorizationManager;
        public ChangeUserRoleCommandHandler(IPromoteUserRole changeUserRole
            , IAuthManager authorizationManager)
        {
            _changeUserRole = changeUserRole;
            _authorizationManager = authorizationManager;
        }

        public async Task<ResultDto<Guid>> Handle(PromoteUserRoleCommand request, CancellationToken cancellationToken)
        {
            //check this admin can change this userRole
            var AuthorizeResult = await _authorizationManager.CheckAccessToUser(request.claims, request.commandRequest.userId);
            if (!AuthorizeResult)
                return ResultDto<Guid>.Failed(ResultDtoMessageLibrary.Forbidden, HttpStatusCode.Unauthorized);

            return await _changeUserRole.PromoteUserRoleAsync(request, cancellationToken);
        }
    }
}
