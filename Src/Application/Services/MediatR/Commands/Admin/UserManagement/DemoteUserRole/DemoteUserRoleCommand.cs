using Application.Interfaces.Auth;
using Application.Services.Implement.Commands.Admin.UserManagement.DemoteUserRole;
using Common.Output;
using MediatR;
using System.Net;
using System.Security.Claims;

namespace Application.Services.MediatR.Commands.Admin.UserManagement.DemoteUserRole
{
    //MediatR command for DemoteUserRoleService
    public record DemoteUserRoleCommandRequest(Guid userId) : IRequest<DemoteUserRoleCommand>;
    public record DemoteUserRoleCommand(DemoteUserRoleCommandRequest commandRequest, IEnumerable<Claim> adminClaims) : IRequest<ResultDto<Guid>>;

    //MediatR commandhandler for DemoteUserRoleService
    public class DemoteUserRoleCommandHandler : IRequestHandler<DemoteUserRoleCommand, ResultDto<Guid>>
    {
        private readonly IDemoteUserRole _demoteUserRole;
        private IAuthManager _authManager;
        public DemoteUserRoleCommandHandler(IDemoteUserRole demoteUserRole
            , IAuthManager authManager)
        {
            _demoteUserRole = demoteUserRole;
            _authManager = authManager;
        }
        public async Task<ResultDto<Guid>> Handle(DemoteUserRoleCommand request, CancellationToken cancellationToken)
        {
            var checkAccess = await _authManager.CheckAccessToUser(request.adminClaims, request.commandRequest.userId);
            if (!checkAccess)
                return ResultDto<Guid>.Failed(ResultDtoMessageLibrary.Forbidden, HttpStatusCode.Unauthorized);

            return await _demoteUserRole.DemoteUserRoleAsync(request, cancellationToken);
        }
    }

}
