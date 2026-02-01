using Application.Interfaces.Auth;
using Application.Services.Implement.Commands.Admin.ServiceManagement.AssignServiceToCompany;
using Common.Output;
using MediatR;
using System.Net;
using System.Security.Claims;

namespace Application.Services.MediatR.Commands.Admin.ServiceManagement.AssignServiceToCompany
{
    //MediatR command for AssignServiceToCompanyService
    public record AssignServiceToCompanyCommandRequest(Guid companyId, Guid serviceId) : IRequest<AssignServiceToCompanyCommand>;
    public record AssignServiceToCompanyCommand(AssignServiceToCompanyCommandRequest commandRequest, IEnumerable<Claim> adminClaims) : IRequest<ResultDto>;

    //MediatR commandHandler for AssignServiceToCompanyService
    public class AssignServiceToCompanyCommandHandler : IRequestHandler<AssignServiceToCompanyCommand, ResultDto>
    {
        private readonly IAuthManager _authManager;
        private readonly IAssignServiceToCompany _assignServiceToCompany;
        public AssignServiceToCompanyCommandHandler(IAuthManager authManager
            , IAssignServiceToCompany assignServiceToCompany)
        {
            _authManager = authManager;
            _assignServiceToCompany = assignServiceToCompany;
        }
        public async Task<ResultDto> Handle(AssignServiceToCompanyCommand request, CancellationToken cancellationToken)
        {
            var checkAccess = await _authManager.CheckAccessToCompany(request.adminClaims, request.commandRequest.companyId);
            if (!checkAccess)
                return ResultDto.Failed(ResultDtoMessageLibrary.Forbidden, HttpStatusCode.Forbidden);

            return await _assignServiceToCompany.AssignServiceToCompanyAsync(request.commandRequest, cancellationToken);
        }
    }
}
