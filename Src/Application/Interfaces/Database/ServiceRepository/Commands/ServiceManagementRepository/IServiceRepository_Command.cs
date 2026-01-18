using Domain.Entities.ServiceManagement;

namespace Application.Interfaces.Database.ServiceRepository.Commands.ServiceManagementRepository
{
    public interface IServiceRepository_Command
    {
        Task AddServiceAsync(Service service);
        Task SaveChangesAsync();
    }
}
