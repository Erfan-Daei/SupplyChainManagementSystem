using Application.Services.MediatR.Queries.Admin.ServiceManagement.GetSupplyRelationListAsSupplier;
using Common.Output;

namespace Application.Services.Implement.Queries.Admin.ServiceManagement.GetSupplyRelationListAsSupplier
{
    public interface IGetSupplyRelationAsSupplier
    {
        Task<ResultDto<List<GetSupplyRelationAsSupplierResultDto>>> GetSupplyRelationAsSupplierAsync(GetSupplyRelationAsSupplierQueryRequest request, CancellationToken ct);
    }
}
