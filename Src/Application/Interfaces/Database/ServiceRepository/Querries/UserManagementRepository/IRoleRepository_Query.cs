using Domain.Entities.UserManagement;

namespace Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository
{
    public interface IRoleRepository_Query   //interface centeralize all Role table Query (Select)  methods
    {
        Task<Role?> GetRoleByNameAsync(string roleName);
    }
}
