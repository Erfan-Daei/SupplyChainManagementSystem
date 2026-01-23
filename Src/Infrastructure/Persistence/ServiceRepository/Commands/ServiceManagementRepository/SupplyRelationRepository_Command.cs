using Application.Interfaces.Database.ServiceRepository.Commands.ServiceManagementRepository;
using Domain.Entities.ServiceManagement;
using Persistence.DatabaseManagement.DatabaseConfiguration.Context;
using Persistence.DatabaseManagement.ExceptionHandler.DatabaseExceptionHandler;

namespace Persistence.ServiceRepository.Commands.ServiceManagementRepository
{
    public class SupplyRelationRepository_Command : ISupplyRelationRepository_Command
    {
        private readonly IDatabaseContext _databaseContext;
        public SupplyRelationRepository_Command(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task AddSupplyRelationAync(SupplyRelation supplyRelation)
        {
            try
            {              
                await _databaseContext.SupplyRelations.AddAsync(supplyRelation);
                await _databaseContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _databaseContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
            }
        }
    }
}
