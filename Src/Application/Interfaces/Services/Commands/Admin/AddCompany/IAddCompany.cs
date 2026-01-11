using Application.MediatR.Services.Commands.Admin.AddCompany;
using Common.Output;

namespace Application.Interfaces.Services.Commands.Admin.AddCompany
{
    public interface IAddCompany
    {
        Task<ResultDto<Guid>> AddCompanyAsync(AddCompanyCommand request, CancellationToken ct);
    }
}
