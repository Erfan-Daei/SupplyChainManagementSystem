using Application.Services.MediatR.Commands.Admin.ServiceManagement.EditCompany;
using Common.Output;

namespace Application.Services.Implement.Commands.Admin.ServiceManagement.EditCompany
{
    public interface IEditCompany
    {
        Task<ResultDto> EditCompanyAsync(EditCompanyCommandRequest request, CancellationToken ct);
    }
}
