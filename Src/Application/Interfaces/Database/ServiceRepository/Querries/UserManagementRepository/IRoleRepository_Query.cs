using Domain.Entities.UserManagement;

namespace Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository
{
    //interface for all Role Query services
    public interface IRoleRepository_Query
    {
        Task<Role?> GetRoleByNameAsync(string roleName);
    }
}
