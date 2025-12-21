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

            builder.HasAlternateKey(sr => new { sr.ServiceId, sr.ConsumerCompanyId });

            builder.Property(sr => sr.SupplyRelationIsActive)
                .HasDefaultValue(true);

            builder.Property(sr => sr.ServiceId)
                .IsRequired();

            builder.Property(sr => sr.ConsumerCompanyId)
                .IsRequired();

            builder.HasOne(sr => sr.Service)
                .WithMany(s => s.SupplyRelations)
                .HasForeignKey(sr => sr.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(sr => sr.ConsumerCompany)
                .WithMany(cc => cc.SupplyRelations)
                .HasForeignKey(sr => sr.ConsumerCompanyId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasQueryFilter(sr => !sr.IsDeleted);
        }
    }
}
