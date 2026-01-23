using Domain.Entities.ServiceManagement;

namespace Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository
{
    //interface for all SupplyRelation Query services
    public interface ISupplyRelationRepository_Query
    {
        Task<List<Guid>?> GetNextRelationsAsync(Guid consumerCompanyId);   //get list of Service SupplierCompanyId for Cycle Detection
        Task<SupplyRelation?> GetSupplyRelationByIdAsync(Guid supplyRelationId);
        Task<SupplyRelation?> GetSupplyRelationDetailByIdAsync(Guid supplyRelationId);
        Task<List<SupplyRelation>?> GetAllCompanySupplyRelationAsync(Guid companyId);   //get all Supplier and Consumer Relation for Company
        Task<List<SupplyRelation>?> GetAllSupplyRelationBySupplierIdAsync(Guid supplierCompanyId);   ////get all Supplier for Company
        Task<List<SupplyRelation>?> GetAllSupplyRelationByConsumerIdAsync(Guid consumerCompanyId);   ////get all Consumer for Company
    }
}
