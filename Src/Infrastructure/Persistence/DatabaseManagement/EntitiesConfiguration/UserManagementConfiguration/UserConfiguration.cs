using Domain.Entities.Common;
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
                .IsUnicode(true);   //to accept persian words

            builder.Property(u => u.UserEmail)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasIndex(u => u.UserEmail)
                .IsUnique();

            builder.Property(u => u.UserEmailConfirmed)
                .HasDefaultValue(false);

            builder.Property(u => u.UserPassword)
                .IsRequired()
                .HasMaxLength(60)
                .IsUnicode(false);

            builder.Property(u => u.UserCompanyId)
                .IsRequired()
                .HasDefaultValue(SeedCompanies.DefaultCompanyId);

            // 1 company to many user relation
            builder.HasOne(u => u.UserCompany)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.UserCompanyId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasQueryFilter(u => !u.IsDeleted);   //for soft delete
        }
    }
}
