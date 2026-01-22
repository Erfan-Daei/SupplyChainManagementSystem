using Application.Services.MediatR.Queries.Admin.ServiceManagement.GetCompanyServiceListAsSupplier;
using Common.Output;

namespace Application.Services.Implement.Queries.Admin.ServiceManagement.GetCompanyServiceListAsSupplier
{
    public interface IGetCompanyServiceListAsSupplier
    {
        Task<ResultDto<List<GetCompanyServiceListAsSupplierResultDto>>> GetCompanyServiceListAsSupplierAsync(GetCompanyServiceListAsSupplierQuery request, CancellationToken ct);
    }
}
