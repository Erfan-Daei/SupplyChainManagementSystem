using Application.Services.MediatR.Queries.Admin.GetCompanyList;
using Common.Output;

namespace Application.Services.Implement.Queries.Admin.GetCompanyList
{
    public interface IGetCompanyList
    {
        Task<ResultDto<List<GetCompanyListResultDto>>> GetCompanyListAsync(GetCompanyListQuery request, CancellationToken ct);
    }
}
