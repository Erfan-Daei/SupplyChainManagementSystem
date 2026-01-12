using Application.Services.MediatR.Commands.Admin.AddCompany;
using Common.Output;

namespace Application.Services.Implement.Commands.Admin
{
    public interface IAddCompany
    {
        Task<ResultDto<Guid>> AddCompanyAsync(AddCompanyCommand request, CancellationToken ct);
    }
}
