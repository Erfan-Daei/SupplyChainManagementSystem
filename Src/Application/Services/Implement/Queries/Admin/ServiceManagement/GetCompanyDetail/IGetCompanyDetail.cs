using Application.Services.MediatR.Queries.Admin.ServiceManagement.GetCompanyDetail;
using Common.Output;

namespace Application.Services.Implement.Queries.Admin.ServiceManagement.GetCompanyDetail
{
    public interface IGetCompanyDetail
    {
        Task<ResultDto<GetCompanyDetailResultDto>> GetCompanyDetailAsync(GetCompanyDetailQuery request, CancellationToken ct);
    }
}
