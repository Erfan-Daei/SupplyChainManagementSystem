using Application.Services.MediatR.Queries.Users.ServiceManagement.GetUnConfirmedSupplyRelationList;
using Common.Output;

namespace Application.Services.Implement.Queries.Users.ServiceManagement.GetUnConfirmedSupplyRelationList
{
    public interface IGetUnConfirmedSupplyRelationList
    {
        Task<ResultDto<List<GetUnConfirmedSupplyRelationListResultDto>>> GetUnConfirmedSupplyRelationListAsync(GetUnConfirmedSupplyRelationListQueryRequest request, CancellationToken ct);
    }
}
