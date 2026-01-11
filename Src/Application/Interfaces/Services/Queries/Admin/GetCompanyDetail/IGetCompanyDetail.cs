using Application.Dtos.Services.Queries.Admin.GetCompanyDetail;
using Application.MediatR.Services.Queries.Admin.GetCompanyDetail;
using Common.Output;

namespace Application.Interfaces.Services.Queries.Admin.GetCompanyDetail
{
    public interface IGetCompanyDetail
    {
        Task<ResultDto<GetCompanyDetailResultDto>> GetCompanyDetailAsync(GetCompanyDetailQuery request, CancellationToken ct);
    }
}
