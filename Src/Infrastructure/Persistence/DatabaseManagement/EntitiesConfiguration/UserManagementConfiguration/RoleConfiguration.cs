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

            //default roles
            builder.HasData(
                new Role { RoleId = SeedRoles.AdminId, RoleName = SeedRoles.AdminName },
                new Role { RoleId = SeedRoles.CompanyAdminId, RoleName = SeedRoles.CompanyAdminName },
                new Role { RoleId = SeedRoles.CompanyUserId, RoleName = SeedRoles.CompanyUserName },
                new Role { RoleId = SeedRoles.ViewerId, RoleName = SeedRoles.ViewerName }
            );

            builder.HasQueryFilter(u => !u.IsDeleted);   //for soft delete
        }
    }
}
