using Domain.Entities.LogManagement;
using Domain.Entities.ServiceManagement;
using Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DatabaseManagement.DatabaseConfiguration.Context
{
    public interface IDatabaseContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<UserInRole> UserInRoles { get; set; }
        DbSet<UserToken> UserTokens { get; set; }
        DbSet<Company> Companies { get; set; }
        DbSet<Service> Services { get; set; }
        DbSet<SupplyRelation> SupplyRelations { get; set; }
        DbSet<Audit> Audits { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
