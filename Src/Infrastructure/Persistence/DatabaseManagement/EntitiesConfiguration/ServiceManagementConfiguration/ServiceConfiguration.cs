using Domain.Entities.ServiceManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DatabaseManagement.EntitiesConfiguration.ServiceManagementConfiguration
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(s => s.ServiceId);

            builder.Property(s => s.ServiceName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);

            builder.Property(s => s.ServiceDescription)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(true);

            builder.Property(s => s.SupplierCompanyId)
                .IsRequired();

            builder.HasOne(s => s.SupplierCompany)
                .WithMany(c => c.Services)
                .HasForeignKey(s => s.SupplierCompanyId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasQueryFilter(s => !s.IsDeleted);
        }
    }
}
