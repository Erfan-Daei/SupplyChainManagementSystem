using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Services.MediatR.Queries.Users.ServiceManagement.GetServiceList;
using Common.Output;
using System.Net;

namespace Application.Services.Implement.Queries.Users.ServiceManagement.GetServiceList
{
    public class GetServiceListService : IGetServiceList
    {
        private readonly IServiceRepository_Query _service_Query;   //GetServiceListAsync
        public GetServiceListService(IServiceRepository_Query service_Query)
        {
            _service_Query = service_Query;
        }
        public async Task<ResultDto<List<GetServiceListResultDto>>> GetServiceListAsync(GetServiceListQuery request, CancellationToken ct)
        {
            try
            {
                var serviceList = await _service_Query.GetServiceListAsync();

                var mappedServiceList = serviceList?.Select(s => new GetServiceListResultDto
                {
                    ServiceId = s.ServiceId,
                    ServiceName = s.ServiceName,
                    ServiceIsActive = s.ServiceIsActive,
                }).ToList() ?? [];

                return ResultDto<List<GetServiceListResultDto>>.Succeeded(mappedServiceList, ResultDtoMessageLibrary.Ok, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResultDto<List<GetServiceListResultDto>>.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
