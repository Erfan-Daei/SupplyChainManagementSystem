using Application.Services.MediatR.Commands.Admin.ServiceManagement.DeleteCompany;
using Common.Output;

namespace Application.Services.Implement.Commands.Admin.ServiceManagement.DeleteCompany
{
    public interface IDeleteCompany
    {
        Task<ResultDto> DeleteCompanyAsync(DeleteCompanyCommand request, CancellationToken ct);
    }
}
