using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Services.MediatR.Queries.Users.ServiceManagement.GetUnConfirmedSupplyRelationList;
using Common.Output;
using System.Net;

namespace Application.Services.Implement.Queries.Users.ServiceManagement.GetUnConfirmedSupplyRelationList
{
    public class GetUnConfirmedSupplyRelationListService : IGetUnConfirmedSupplyRelationList
    {
        private readonly ISupplyRelationRepository_Query _supplyRelation_Query;   //GetAllUnConfirmedSupplyRelationByCompanyIdAsync
        public GetUnConfirmedSupplyRelationListService(ISupplyRelationRepository_Query supplyRelation_Query)
        {
            _supplyRelation_Query = supplyRelation_Query;
        }
        public async Task<ResultDto<List<GetUnConfirmedSupplyRelationListResultDto>>> GetUnConfirmedSupplyRelationListAsync(GetUnConfirmedSupplyRelationListQueryRequest request, CancellationToken ct)
        {
            try
            {
                var supplyRelationList = await _supplyRelation_Query.GetAllUnConfirmedSupplyRelationByCompanyIdAsync(request.companyId);

                var mappedSupplyRelationList = supplyRelationList?.Select(sr => new GetUnConfirmedSupplyRelationListResultDto
                {
                    SupplyRelationId = sr.SupplyRelationId,
                    SupplierCompanyName = sr.SupplierCompany.CompanyName,
                    ConsumerCompanyName = sr.ConsumerCompany.CompanyName,
                    ServiceName = sr.Service.ServiceName,
                    CreatedAt = sr.CreatedAt,
                }).ToList() ?? [];

                return ResultDto<List<GetUnConfirmedSupplyRelationListResultDto>>.Succeeded(mappedSupplyRelationList, ResultDtoMessageLibrary.Ok, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResultDto<List<GetUnConfirmedSupplyRelationListResultDto>>.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
