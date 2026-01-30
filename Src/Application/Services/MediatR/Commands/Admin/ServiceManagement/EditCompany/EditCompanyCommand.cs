using Application.Interfaces.Auth;
using Application.Services.Implement.Commands.Admin.ServiceManagement.EditCompany;
using Common.Output;
using MediatR;
using System.Net;
using System.Security.Claims;

namespace Application.Services.MediatR.Commands.Admin.ServiceManagement.EditCompany
{
    //MediatR Command for EditCompanyService
    public record EditCompanyCommandRequest(Guid companyId, string companyName) : IRequest<EditCompanyCommand>;
    public record EditCompanyCommand(EditCompanyCommandRequest commandRequest, IEnumerable<Claim> adminClaims) : IRequest<ResultDto>;

    //MediatR Command Handler for EditCompanyService
    public class EditCompanyCommandHandler : IRequestHandler<EditCompanyCommand, ResultDto>
    {
        private readonly IEditCompany _editCompany;
        private readonly IAuthManager _authManager;
        public EditCompanyCommandHandler(IEditCompany editCompany
            , IAuthManager authManager)
        {
            _editCompany = editCompany;
            _authManager = authManager;
        }

        public async Task<ResultDto> Handle(EditCompanyCommand request, CancellationToken cancellationToken)
        {
            var checkAccess = await _authManager.CheckAccessToCompany(request.adminClaims, request.commandRequest.companyId);
            if (!checkAccess)
                return ResultDto.Failed(ResultDtoMessageLibrary.Forbidden, HttpStatusCode.Unauthorized);

            return await _editCompany.EditCompanyAsync(request.commandRequest, cancellationToken);
        }
    }
}
