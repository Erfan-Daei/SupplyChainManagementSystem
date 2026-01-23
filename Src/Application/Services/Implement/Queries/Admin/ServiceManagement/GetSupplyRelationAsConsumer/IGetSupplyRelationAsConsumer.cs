using Application.Services.MediatR.Queries.Admin.ServiceManagement.GetSupplyRelationAsConsumer;
using Common.Output;

namespace Application.Services.Implement.Queries.Admin.ServiceManagement.GetSupplyRelationAsConsumer
{
    public interface IGetSupplyRelationAsConsumer
    {
        Task<ResultDto<List<GetSupplyRelationAsConsumerResultDto>>> GetSupplyRelationAsConsumerAsync(GetSupplyRelationAsConsumerQueryRequest request, CancellationToken ct);
    }
}
