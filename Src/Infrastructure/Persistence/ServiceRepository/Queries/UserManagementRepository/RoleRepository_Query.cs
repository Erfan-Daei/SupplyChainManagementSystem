using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseManagement.DatabaseConfiguration;

namespace Persistence.ServiceRepository.Queries.UserManagementRepository
{
    ////implemented class to centeralize all Role table Query (Select)  methods
    public class RoleRepository_Query : IRoleRepository_Query
    {
        private readonly DatabaseContext _databaseContext;
        public RoleRepository_Query(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Role?> GetRoleByNameAsync(string roleName)
        {
            var role = await _databaseContext.Roles
                .FirstOrDefaultAsync(r => r.RoleName == roleName);

            return role;
        }
    }
}
