using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Persistence.ExceptionHandler.DatabaseExceptionHandler;
using Persistence.Interface.DatabaseManagement.DatabaseConfiguration;

namespace Persistence.ServiceRepository.Queries.UserManagementRepository
{
    ////implemented class to centeralize all Role table Query (Select)  methods
    public class RoleRepository_Query : IRoleRepository_Query
    {
        private readonly IDatabaseContext _databaseContext;
        public RoleRepository_Query(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Role?> GetRoleByNameAsync(string roleName)
        {
            try
            {
                var role = await _databaseContext.Roles
                .FirstOrDefaultAsync(r => r.RoleName.ToLower() == roleName.ToLower());

                return role;
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
                return null;
            }
        }
    }
}
