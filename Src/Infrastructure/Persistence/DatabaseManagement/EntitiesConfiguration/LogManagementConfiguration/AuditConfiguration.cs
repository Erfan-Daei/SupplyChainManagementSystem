using Domain.Entities.LogManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DatabaseManagement.EntitiesConfiguration.LogManagementConfiguration
{
    public class AuditConfiguration : IEntityTypeConfiguration<Audit>
    {
        public void Configure(EntityTypeBuilder<Audit> builder)
        {
            builder.HasKey(a => a.AuditId);

            builder.Property(a => a.UserId)
                .IsRequired();

            builder.Property(a => a.UserFullName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.RoleId)
                .IsRequired();

            builder.Property(a => a.RoleName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.Action)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(a => a.ActionOnEntity)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(a => a.ActionAtTime)
                .IsRequired()
                .HasDefaultValue(DateTime.UtcNow);
        }
    }
}
