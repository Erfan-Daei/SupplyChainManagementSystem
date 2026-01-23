using Application.Services.MediatR.Commands.Admin.ServiceManagement.UnAssignServiceFromCompany;
using Common.Output;

namespace Application.Services.Implement.Commands.Admin.ServiceManagement.UnAssignServiceFromCompany
{
    public interface IUnAssignServiceFromCompany
    {
        Task<ResultDto> UnAssignServiceFromCompanyAsync(UnAssignServiceFromCompanyCommandRequest request, CancellationToken ct);
    }
}
