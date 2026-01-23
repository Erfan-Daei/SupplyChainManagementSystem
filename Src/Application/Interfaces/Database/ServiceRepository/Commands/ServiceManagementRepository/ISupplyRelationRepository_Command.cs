using Domain.Entities.ServiceManagement;

namespace Application.Interfaces.Database.ServiceRepository.Commands.ServiceManagementRepository
{
    //interface for all SupplyRelation Command services
    public interface ISupplyRelationRepository_Command
    {
        Task AddSupplyRelationAync(SupplyRelation supplyRelation);
        Task SaveChangesAsync();
    }
}
