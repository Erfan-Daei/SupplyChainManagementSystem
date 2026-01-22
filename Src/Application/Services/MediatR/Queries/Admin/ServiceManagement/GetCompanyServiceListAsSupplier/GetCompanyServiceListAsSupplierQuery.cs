using Application.Services.Implement.Queries.Admin.ServiceManagement.GetCompanyServiceListAsSupplier;
using Common.Output;
using MediatR;

namespace Application.Services.MediatR.Queries.Admin.ServiceManagement.GetCompanyServiceListAsSupplier
{
    //MediatR query for GetCompanyServiceListAsSupplierService
    public record GetCompanyServiceListAsSupplierQuery(Guid companyId) : IRequest<ResultDto<List<GetCompanyServiceListAsSupplierResultDto>>>;

    ////MediatR queryHandler for GetCompanyServiceListAsSupplierService
    public class GetCompanyServiceListAsSupplierQueryHandler : IRequestHandler<GetCompanyServiceListAsSupplierQuery, ResultDto<List<GetCompanyServiceListAsSupplierResultDto>>>
    {
        private readonly IGetCompanyServiceListAsSupplier _getCompanyServiceListAsSupplier;
        public GetCompanyServiceListAsSupplierQueryHandler(IGetCompanyServiceListAsSupplier getCompanyServiceListAsSupplier)
        {
            _getCompanyServiceListAsSupplier = getCompanyServiceListAsSupplier;
        }
        public async Task<ResultDto<List<GetCompanyServiceListAsSupplierResultDto>>> Handle(GetCompanyServiceListAsSupplierQuery request, CancellationToken cancellationToken)
        {
            return await _getCompanyServiceListAsSupplier.GetCompanyServiceListAsSupplierAsync(request, cancellationToken);
        }
    }
}
