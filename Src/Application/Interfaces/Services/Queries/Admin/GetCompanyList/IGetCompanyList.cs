using Application.Dtos.Services.Queries.Admin.GetCompanyList;
using Application.MediatR.Services.Queries.Admin.GetCompanyList;
using Common.Output;

namespace Application.Interfaces.Services.Queries.Admin.GetCompanyList
{
    public interface IGetCompanyList
    {
        Task<ResultDto<List<GetCompanyListResultDto>>> GetCompanyListAsync(GetCompanyListQuery request, CancellationToken ct);
    }
}
