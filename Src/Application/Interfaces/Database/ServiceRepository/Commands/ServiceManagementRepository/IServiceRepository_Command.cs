using Domain.Entities.ServiceManagement;

namespace Application.Interfaces.Database.ServiceRepository.Commands.ServiceManagementRepository
{
    //interface for all Service Command services
    public interface IServiceRepository_Command
    {
        Task AddServiceAsync(Service service);
        Task SaveChangesAsync();
    }
}
