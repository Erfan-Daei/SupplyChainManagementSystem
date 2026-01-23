using Application.Services.MediatR.Commands.Admin.ServiceManagement.AssignServiceToCompany;
using Common.Output;

namespace Application.Services.Implement.Commands.Admin.ServiceManagement.AssignServiceToCompany
{
    public interface IAssignServiceToCompany
    {
        Task<ResultDto> AssignServiceToCompanyAsync(AssignServiceToCompanyCommandRequest request, CancellationToken ct);
    }
}
