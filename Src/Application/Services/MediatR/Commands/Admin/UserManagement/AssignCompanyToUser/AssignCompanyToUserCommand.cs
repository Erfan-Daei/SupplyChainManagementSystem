using Application.Interfaces.Auth;
using Application.Services.Implement.Commands.Admin.UserManagement.AssignCompanyToUser;
using Common.Output;
using MediatR;
using System.Net;
using System.Security.Claims;

namespace Application.Services.MediatR.Commands.Admin.UserManagement.AssignCompanyToUser
{
    //MediatR command for AssignCompanyToUserService
    public record AssignCompanyToUserCommandRequest(Guid userId, Guid companyId) : IRequest<AssignCompanyToUserCommand>;
    public record AssignCompanyToUserCommand(AssignCompanyToUserCommandRequest commandRequest, IEnumerable<Claim> adminClaims) : IRequest<ResultDto>;

    //MediatR commandHandler for AssignCompanyToUserService
    public class AssignCompanyToUserCommandHandler : IRequestHandler<AssignCompanyToUserCommand, ResultDto>
    {
        private readonly IAuthManager _authManager;
        private readonly IAssignCompanyToUser _assignCompanyToUser;
        public AssignCompanyToUserCommandHandler(IAuthManager authManager
            , IAssignCompanyToUser assignCompanyToUser)
        {
            _authManager = authManager;
            _assignCompanyToUser = assignCompanyToUser;
        }
        public async Task<ResultDto> Handle(AssignCompanyToUserCommand request, CancellationToken cancellationToken)
        {
            /*var checkAccess = await _authManager.ChechAccessToCompany(request.adminClaims, request.commandRequest.companyId);
            if (!checkAccess)
                return ResultDto.Failed(ResultDtoMessageLibrary.UnAuthorized, HttpStatusCode.Unauthorized);*/

            return await _assignCompanyToUser.AssignCompanyToUserAsync(request.commandRequest, cancellationToken);
        }
    }
}
