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
                .IsUnicode(true);   //to accept persian words

            builder.Property(s => s.ServiceDescription)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(true);   //to accept persian words

            builder.Property(s => s.ServiceIsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(s => s.SupplierCompanyId)
                .IsRequired(true);

            builder.HasIndex(s => s.SupplierCompanyId)
                .IsUnique(false);

            builder.HasQueryFilter(s => !s.IsDeleted);   //for soft delete
        }
    }
}
