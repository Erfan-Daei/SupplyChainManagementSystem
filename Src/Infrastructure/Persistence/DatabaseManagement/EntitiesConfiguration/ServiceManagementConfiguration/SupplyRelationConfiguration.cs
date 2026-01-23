using Domain.Entities.ServiceManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DatabaseManagement.EntitiesConfiguration.ServiceManagementConfiguration
{
    public class SupplyRelationConfiguration : IEntityTypeConfiguration<SupplyRelation>
    {
        public void Configure(EntityTypeBuilder<SupplyRelation> builder)
        {
            builder.HasKey(sr => sr.SupplyRelationId);

            builder.Property(sr => sr.SupplyRelationIsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(sr => sr.SupplyRelationIsConfirmed)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(sr => sr.ServiceId)
                .IsRequired();

            builder.Property(sr => sr.SupplierCompanyId)
                .IsRequired();

            builder.Property(sr => sr.ConsumerCompanyId)
                .IsRequired();

            // 1 service to many supplyRelation relation
            builder.HasOne(sr => sr.Service)
                .WithMany(s => s.SupplyRelations)
                .HasForeignKey(sr => sr.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);   //to avoid delete

            // 1 supplierCompany to many supplyRelation relation
            builder.HasOne(sr => sr.SupplierCompany)
                .WithMany(cc => cc.SupplyRelationsAsSupplier)
                .HasForeignKey(sr => sr.SupplierCompanyId)
                .OnDelete(DeleteBehavior.NoAction);   //to avoid delete

            // 1 consumerCompany to many supplyRelation relation
            builder.HasOne(sr => sr.ConsumerCompany)
                .WithMany(cc => cc.SupplyRelationsAsConsumer)
                .HasForeignKey(sr => sr.ConsumerCompanyId)
                .OnDelete(DeleteBehavior.NoAction);   //to avoid delete

            builder.HasQueryFilter(sr => !sr.IsDeleted);   //for soft delete
        }
    }
}
