using Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DatabaseManagement.EntitiesConfiguration.UserManagementConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);


            builder.Property(u => u.UserFullName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);

            builder.Property(u => u.UserEmail)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasIndex(u => u.UserEmail)
                .IsUnique();

            builder.Property(u => u.UserEmailConfirmed)
                .HasDefaultValue(false);

            builder.Property(u => u.CompanyId)
                .IsRequired();

            builder.HasOne(u => u.Company)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.CompanyId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasQueryFilter(u => !u.IsDeleted);
        }
    }
}
