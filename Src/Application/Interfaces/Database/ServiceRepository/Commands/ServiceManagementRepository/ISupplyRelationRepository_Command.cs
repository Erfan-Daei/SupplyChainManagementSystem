using Domain.Entities.ServiceManagement;

namespace Application.Interfaces.Database.ServiceRepository.Commands.ServiceManagementRepository
{
    public interface ISupplyRelationRepository_Command
    {
        Task AddSupplyRelationAync(SupplyRelation supplyRelation);
    }
}
