using Application.Services.MediatR.Queries.Admin.GetCompanyDetail;
using Common.Output;

namespace Application.Services.Implement.Queries.Admin.GetCompanyDetail
{
    public interface IGetCompanyDetail
    {
        Task<ResultDto<GetCompanyDetailResultDto>> GetCompanyDetailAsync(GetCompanyDetailQuery request, CancellationToken ct);
    }
}
