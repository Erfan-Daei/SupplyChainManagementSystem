using Application.Services.MediatR.Queries.Users.ServiceManagement.GetServiceDetail;
using Common.Output;

namespace Application.Services.Implement.Queries.Users.ServiceManagement.GetServiceDetail
{
    public interface IGetServiceDetail
    {
        Task<ResultDto<GetServiceDetailResultDto>> GetServiceDetailAsync(GetServiceDetailQuery request, CancellationToken ct);
    }
}
