using Application.Services.MediatR.Queries.Users.ServiceManagement.GetServiceList;
using Common.Output;

namespace Application.Services.Implement.Queries.Users.ServiceManagement.GetServiceList
{
    public interface IGetServiceList
    {
        Task<ResultDto<List<GetServiceListResultDto>>> GetServiceListAsync(GetServiceListQuery request, CancellationToken ct);
    }
}
