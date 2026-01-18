using Domain.Entities.ServiceManagement;

namespace Application.Interfaces.Database.ServiceRepository.Commands.ServiceManagementRepository
{
    //interface for all Company Command services
    public interface ICompanyRepository_Command
    {
        Task AddCompanyAsync(Company company);
        Task SaveChangesAsync();
    }
}
