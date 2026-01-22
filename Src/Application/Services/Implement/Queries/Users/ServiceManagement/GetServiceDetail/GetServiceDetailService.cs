using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Services.MediatR.Queries.Users.ServiceManagement.GetServiceDetail;
using Common.Output;
using System.Net;

namespace Application.Services.Implement.Queries.Users.ServiceManagement.GetServiceDetail
{
    public class GetServiceDetailService : IGetServiceDetail
    {
        private readonly IServiceRepository_Query _service_Query;   //GetServiceByIdAsync
        public GetServiceDetailService(IServiceRepository_Query service_Query)
        {
            _service_Query = service_Query;
        }
        public async Task<ResultDto<GetServiceDetailResultDto>> GetServiceDetailAsync(GetServiceDetailQuery request, CancellationToken ct)
        {
            try
            {
                var service = await _service_Query.GetServiceDetailAsync(request.serviceId);
                if (service == null)
                    return ResultDto<GetServiceDetailResultDto>.Failed(ResultDtoMessageLibrary.ServiceNotFound, HttpStatusCode.NotFound);

                return ResultDto<GetServiceDetailResultDto>.Succeeded(new GetServiceDetailResultDto
                {
                    Creator = service.CreatorCompanyId,
                    ServiceName = service.ServiceName,
                    ServiceDescription = service.ServiceDescription,
                    ServiceIsActive = service.ServiceIsActive,
                    CreatedAt = service.CreatedAt,
                    SupplyRelationCount = service.SupplyRelations.Count,
                    SupplierCompanies = service.SupplierCompanies.Select(sc => new GetServiceDetailSupplierCompanyDto
                    {
                        CompanyId = sc.CompanyId,
                        CompanyName = sc.CompanyName,
                    }).ToList() ?? []
                }, ResultDtoMessageLibrary.Ok, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResultDto<GetServiceDetailResultDto>.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
