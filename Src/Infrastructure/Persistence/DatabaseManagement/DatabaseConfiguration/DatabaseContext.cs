using Application.Interfaces.Database;
using Domain.Entities.LogManagement;
using Domain.Entities.ServiceManagement;
using Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Persistence.DatabaseManagement.DatabaseConfiguration
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        private readonly IDatabaseContext_UserInfo _databaseContext_UserInfo;
        public DatabaseContext(DbContextOptions<DatabaseContext> options, IDatabaseContext_UserInfo databaseContext_UserInfotainDBUser) : base(options)
        {
            _databaseContext_UserInfo = databaseContext_UserInfotainDBUser;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInRole> UserInRoles { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<SupplyRelation> SupplyRelations { get; set; }
        public DbSet<Audit> Audits { get; set; }

        IQueryable<User> IDatabaseContext.Users => Users;

        IQueryable<Role> IDatabaseContext.Roles => Roles;

        IQueryable<UserInRole> IDatabaseContext.UserInRoles => UserInRoles;

        IQueryable<Company> IDatabaseContext.Companies => Companies;

        IQueryable<Service> IDatabaseContext.Services => Services;

        IQueryable<SupplyRelation> IDatabaseContext.SupplyRelations => SupplyRelations;

        IQueryable<Audit> IDatabaseContext.Audits => Audits;

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                e.State == EntityState.Modified ||
                e.State == EntityState.Deleted);

            foreach (var entry in ChangeTracker.Entries<Audit>())
            {
                if (entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
                {
                    throw new InvalidOperationException("Audit logs cannot be modified or deleted.");
                }
            }

            foreach (var entry in entries)
            {
                var audit = new Audit(
                userId: _databaseContext_UserInfo.UserId,          // از context یا service می‌گیری
                userFullName: _databaseContext_UserInfo.UserFullName,
                roleId: _databaseContext_UserInfo.RoleId,
                roleName: _databaseContext_UserInfo.RoleName,
                action: entry.State.ToString(),  // Created, Updated, Deleted
                actionOnEntity: entry.Entity.GetType().Name
                );

                Audits.Add(audit);
            }
            return base.SaveChangesAsync(cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    }
}
