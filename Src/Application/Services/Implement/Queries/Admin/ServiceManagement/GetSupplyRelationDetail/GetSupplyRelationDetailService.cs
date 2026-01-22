using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Services.MediatR.Queries.Admin.ServiceManagement.GetSupplyRelationDetail;
using Common.Output;
using System.Net;

namespace Application.Services.Implement.Queries.Admin.ServiceManagement.GetSupplyRelationDetail
{
    public class GetSupplyRelationDetailService : IGetSupplyRelationDetail
    {
        private readonly ISupplyRelationRepository_Query _supplyRelation_Query;
        public GetSupplyRelationDetailService(ISupplyRelationRepository_Query supplyRelation_Query)
        {
            _supplyRelation_Query = supplyRelation_Query;
        }
        public async Task<ResultDto<GetSupplyRelationDetailResultDto>> GetSupplyRelationDetailAsync(GetSupplyRelationDetailQueryRequest request, CancellationToken ct)
        {
            try
            {
                var supplyRelation = await _supplyRelation_Query.GetSupplyRelationDetailByIdAsync(request.supplyRelationId);
                if (supplyRelation == null)
                    return ResultDto<GetSupplyRelationDetailResultDto>.Failed(ResultDtoMessageLibrary.SupplyRelationNotFound, HttpStatusCode.NotFound);

                return ResultDto<GetSupplyRelationDetailResultDto>.Succeeded(new GetSupplyRelationDetailResultDto
                {
                    SupplyRelationIsActive = supplyRelation.SupplyRelationIsActive,
                    ServiceId = supplyRelation.ServiceId,
                    ServiceName = supplyRelation.Service.ServiceName,
                    ServiceDescription = supplyRelation.Service.ServiceDescription,
                    SupplierCompanyId = supplyRelation.SupplierCompanyId,
                    SupplierCompanyName = supplyRelation.SupplierCompany.CompanyName,
                    ConsumerCompanyId = supplyRelation.ConsumerCompanyId,
                    ConsumerCompanyName = supplyRelation.ConsumerCompany.CompanyName
                }, ResultDtoMessageLibrary.Ok, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResultDto<GetSupplyRelationDetailResultDto>.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
