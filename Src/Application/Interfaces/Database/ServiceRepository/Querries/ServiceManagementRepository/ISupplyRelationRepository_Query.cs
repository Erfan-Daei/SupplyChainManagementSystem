using Domain.Entities.ServiceManagement;

namespace Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository
{
    //interface for all SupplyRelation Query services
    public interface ISupplyRelationRepository_Query
    {
        Task<List<Guid>?> GetNextRelationsAsync(Guid consumerCompanyId);
        Task<SupplyRelation?> GetSupplyRelationByIdAsync(Guid supplyRelationId);
        Task<SupplyRelation?> GetSupplyRelationDetailByIdAsync(Guid supplyRelationId);
    }
}
