using Application.Services.MediatR.Queries.Admin.ServiceManagement.GetCompanyList;
using Common.Output;

namespace Application.Services.Implement.Queries.Admin.ServiceManagement.GetCompanyList
{
    public interface IGetCompanyList
    {
        Task<ResultDto<List<GetCompanyListResultDto>>> GetCompanyListAsync(GetCompanyListQuery request, CancellationToken ct);
    }
}
