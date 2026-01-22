using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Services.MediatR.Queries.Admin.ServiceManagement.GetCompanyServiceListAsSupplier;
using Common.Output;
using System.Net;

namespace Application.Services.Implement.Queries.Admin.ServiceManagement.GetCompanyServiceListAsSupplier
{
    public class GetCompanyServiceListAsSupplierService : IGetCompanyServiceListAsSupplier
    {
        private readonly ICompanyRepository_Query _company_Query;
        public GetCompanyServiceListAsSupplierService(ICompanyRepository_Query company_Query)
        {
            _company_Query = company_Query;
        }
        public async Task<ResultDto<List<GetCompanyServiceListAsSupplierResultDto>>> GetCompanyServiceListAsSupplierAsync(GetCompanyServiceListAsSupplierQuery request, CancellationToken ct)
        {
            try
            {
                var serviceList = await _company_Query.GetServiceListFromSupplierIdAsync(request.companyId);
                if (serviceList == null)
                    return ResultDto<List<GetCompanyServiceListAsSupplierResultDto>>.Failed(ResultDtoMessageLibrary.ServiceNotFound, HttpStatusCode.NotFound);

                var mappedServiceList = serviceList.Select(s => new GetCompanyServiceListAsSupplierResultDto
                {
                    ServiceId = s.ServiceId,
                    ServiceName = s.ServiceName,
                    ServiceIsActive = s.ServiceIsActive
                }).ToList();

                return ResultDto<List<GetCompanyServiceListAsSupplierResultDto>>.Succeeded(mappedServiceList, ResultDtoMessageLibrary.Ok, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResultDto<List<GetCompanyServiceListAsSupplierResultDto>>.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
