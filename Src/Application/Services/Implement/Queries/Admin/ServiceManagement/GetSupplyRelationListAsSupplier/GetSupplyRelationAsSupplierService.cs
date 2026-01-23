using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Services.MediatR.Queries.Admin.ServiceManagement.GetSupplyRelationListAsSupplier;
using Common.Output;
using System.Net;

namespace Application.Services.Implement.Queries.Admin.ServiceManagement.GetSupplyRelationListAsSupplier
{
    public class GetSupplyRelationAsSupplierService : IGetSupplyRelationAsSupplier
    {
        private readonly ISupplyRelationRepository_Query _supplyRelation_Query;   //GetAllSupplyRelationBySupplierIdAsync
        public GetSupplyRelationAsSupplierService(ISupplyRelationRepository_Query supplyRelation_Query)
        {
            _supplyRelation_Query = supplyRelation_Query;
        }
        public async Task<ResultDto<List<GetSupplyRelationAsSupplierResultDto>>> GetSupplyRelationAsSupplierAsync(GetSupplyRelationAsSupplierQueryRequest request, CancellationToken ct)
        {
            try
            {
                var supplyRelationList = await _supplyRelation_Query.GetAllSupplyRelationBySupplierIdAsync(request.companyId);

                return ResultDto<List<GetSupplyRelationAsSupplierResultDto>>.Succeeded(supplyRelationList?.Select(sr => new GetSupplyRelationAsSupplierResultDto
                {
                    SupplyRelationId = sr.SupplyRelationId,
                    ConsumerCompanyName = sr.ConsumerCompany.CompanyName,
                    ServiceName = sr.Service.ServiceName,
                    SupplyRelationIsConfirmed = sr.SupplyRelationIsConfirmed
                }).ToList() ?? []
                , ResultDtoMessageLibrary.Ok, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResultDto<List<GetSupplyRelationAsSupplierResultDto>>.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
