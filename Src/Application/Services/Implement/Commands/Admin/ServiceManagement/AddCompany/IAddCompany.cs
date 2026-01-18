using Application.Services.MediatR.Commands.Admin.ServiceManagement.AddCompany;
using Common.Output;

namespace Application.Services.Implement.Commands.Admin.ServiceManagement.AddCompany
{
    public interface IAddCompany
    {
        Task<ResultDto<Guid>> AddCompanyAsync(AddCompanyCommand request, CancellationToken ct);
    }
}
