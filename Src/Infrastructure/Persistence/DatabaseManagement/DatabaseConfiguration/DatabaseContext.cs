using Domain.Entities.LogManagement;
using Domain.Entities.ServiceManagement;
using Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Persistence.DatabaseManagement.DatabaseConfiguration
{
    public class DatabaseContext : DbContext
    {
        private readonly DatabaseContextAuditManager _auditManager;
        public DatabaseContext(DbContextOptions<DatabaseContext> options, DatabaseContextAuditManager auditManager) : base(options)
        {
            _auditManager = auditManager;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInRole> UserInRoles { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<SupplyRelation> SupplyRelations { get; set; }
        public DbSet<Audit> Audits { get; set; }


        //override saveChanges function to automatically save any database changes in Audit table
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                e.State == EntityState.Modified ||
                e.State == EntityState.Deleted);

            //create list of Audit to save in database
            var audits = _auditManager.CreateAudits(entries);

            if (audits.Any())
                Audits.AddRange(audits);

            return await base.SaveChangesAsync(cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //auto get entity configuration classes "IEntityTypeConfiguration"
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //avoid HasDefaultValue() funtion error in database migrations
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    }
}
