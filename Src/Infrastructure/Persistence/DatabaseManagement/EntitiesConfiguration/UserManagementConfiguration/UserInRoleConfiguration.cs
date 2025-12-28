using Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DatabaseManagement.EntitiesConfiguration.UserManagementConfiguration
{
    public class UserInRoleConfiguration : IEntityTypeConfiguration<UserInRole>
    {
        public void Configure(EntityTypeBuilder<UserInRole> builder)
        {
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

            // 1 user to 1 userInRole relation
            builder.HasOne(ur => ur.User)
                .WithOne(u => u.UserInRole)
                .OnDelete(DeleteBehavior.NoAction);   //to avoid delete

            // 1 role to many userInRole relation
            builder.HasOne(ur => ur.Role)
                .WithMany(r => r.UserInRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.NoAction);   //to avoid delete

            builder.HasQueryFilter(u => !u.IsDeleted);   //for soft delete
        }
    }
}
