using Application.Services.MediatR.Queries.Admin.ServiceManagement.GetSupplyRelationDetail;
using Common.Output;

namespace Application.Services.Implement.Queries.Admin.ServiceManagement.GetSupplyRelationDetail
{
    public interface IGetSupplyRelationDetail
    {
        Task<ResultDto<GetSupplyRelationDetailResultDto>> GetSupplyRelationDetailAsync(GetSupplyRelationDetailQueryRequest request, CancellationToken ct);
    }
}
