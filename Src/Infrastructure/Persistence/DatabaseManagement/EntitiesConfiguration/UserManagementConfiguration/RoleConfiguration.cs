using Common.Domain_Commons;
using Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DatabaseManagement.EntitiesConfiguration.UserManagementConfiguration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.RoleId);

            builder.Property(r => r.RoleName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(r => r.CreatedAt)
                .HasDefaultValue(DateTime.UtcNow);

            builder.HasData(
                new Role { RoleId = Guid.NewGuid(), RoleName = SeedRoles.Admin.ToString() },
                new Role { RoleId = Guid.NewGuid(), RoleName = SeedRoles.CompanyAdmin.ToString() },
                new Role { RoleId = Guid.NewGuid(), RoleName = SeedRoles.CompanyUser.ToString() },
                new Role { RoleId = Guid.NewGuid(), RoleName = SeedRoles.Viewer.ToString() }
            );

            builder.HasQueryFilter(u => !u.IsDeleted);
        }
    }
}
