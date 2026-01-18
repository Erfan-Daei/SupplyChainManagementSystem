using Application.Interfaces.Database.ServiceRepository.Commands.ServiceManagementRepository;
using Domain.Entities.ServiceManagement;
using Persistence.DatabaseManagement.DatabaseConfiguration.Context;
using Persistence.DatabaseManagement.ExceptionHandler.DatabaseExceptionHandler;

namespace Persistence.ServiceRepository.Commands.ServiceManagementRepository
{
    public class ServiceRepository_Command : IServiceRepository_Command
    {
        private readonly IDatabaseContext _databaseContext;
        public ServiceRepository_Command(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task AddServiceAsync(Service service)
        {
            try
            {
                await _databaseContext.Services.AddAsync(service);
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
