using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Services.MediatR.Queries.Admin.ServiceManagement.GetSupplyRelationAsConsumer;
using Common.Output;
using System.Net;

namespace Application.Services.Implement.Queries.Admin.ServiceManagement.GetSupplyRelationAsConsumer
{
    public class GetSupplyRelationAsConsumerService : IGetSupplyRelationAsConsumer
    {
        private readonly ISupplyRelationRepository_Query _supplyRelation_Query;   //GetAllSupplyRelationByConsumerIdAsync
        public GetSupplyRelationAsConsumerService(ISupplyRelationRepository_Query supplyRelation_Query)
        {
            _supplyRelation_Query = supplyRelation_Query;
        }
        public async Task<ResultDto<List<GetSupplyRelationAsConsumerResultDto>>> GetSupplyRelationAsConsumerAsync(GetSupplyRelationAsConsumerQueryRequest request, CancellationToken ct)
        {
            try
            {
                var supplyRelationList = await _supplyRelation_Query.GetAllSupplyRelationByConsumerIdAsync(request.companyId);

                return ResultDto<List<GetSupplyRelationAsConsumerResultDto>>.Succeeded(supplyRelationList?.Select(sr => new GetSupplyRelationAsConsumerResultDto
                {
                    SupplyRelationId = sr.SupplyRelationId,
                    SupplierCompanyName = sr.SupplierCompany.CompanyName,
                    ServiceName = sr.Service.ServiceName,
                    SupplyRelationIsConfirmed = sr.SupplyRelationIsConfirmed
                }).ToList() ?? []
                , ResultDtoMessageLibrary.Ok, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResultDto<List<GetSupplyRelationAsConsumerResultDto>>.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
