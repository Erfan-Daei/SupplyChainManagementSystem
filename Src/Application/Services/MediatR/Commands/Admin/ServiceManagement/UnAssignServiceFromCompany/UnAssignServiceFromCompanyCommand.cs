using Application.Interfaces.Auth;
using Application.Services.Implement.Commands.Admin.ServiceManagement.UnAssignServiceFromCompany;
using Common.Output;
using MediatR;
using System.Net;
using System.Security.Claims;

namespace Application.Services.MediatR.Commands.Admin.ServiceManagement.UnAssignServiceFromCompany
{
    //MediatR command for UnAssignServiceFromCompanyService
    public record UnAssignServiceFromCompanyCommandRequest(Guid companyId, Guid serviceId) : IRequest<UnAssignServiceFromCompanyCommand>;
    public record UnAssignServiceFromCompanyCommand(UnAssignServiceFromCompanyCommandRequest commandRequest, IEnumerable<Claim> adminClaims) : IRequest<ResultDto>;

    ////MediatR commandHandler for UnAssignServiceFromCompanyService
    public class UnAssignServiceFromCompanyCommandHandler : IRequestHandler<UnAssignServiceFromCompanyCommand, ResultDto>
    {
        private readonly IAuthManager _authManager;
        private readonly IUnAssignServiceFromCompany _unAssignServiceFromCompany;
        public UnAssignServiceFromCompanyCommandHandler(IAuthManager authManager
            , IUnAssignServiceFromCompany unAssignServiceFromCompany)
        {
            _authManager = authManager;
            _unAssignServiceFromCompany = unAssignServiceFromCompany;
        }
        public async Task<ResultDto> Handle(UnAssignServiceFromCompanyCommand request, CancellationToken cancellationToken)
        {
            /*var checkAccess = await _authManager.CheckAccessToCompany(request.adminClaims, request.commandRequest.companyId);
            if (!checkAccess)
                return ResultDto.Failed(ResultDtoMessageLibrary.UnAuthorized, HttpStatusCode.Unauthorized);*/

            return await _unAssignServiceFromCompany.UnAssignServiceFromCompanyAsync(request.commandRequest, cancellationToken);
        }
    }
}
