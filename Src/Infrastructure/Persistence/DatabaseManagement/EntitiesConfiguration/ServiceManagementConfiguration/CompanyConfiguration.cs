using Domain.Entities.Common;
using Domain.Entities.ServiceManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DatabaseManagement.EntitiesConfiguration.ServiceManagementConfiguration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(c => c.CompanyId);

            builder.Property(c => c.CompanyName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);   //to accept persian words

            //many companies to many services
            builder.HasMany(c => c.Services)
                .WithMany(s => s.SupplierCompanies);

            builder.HasData(
                new Company { CompanyId = SeedCompanies.DefaultCompanyId, CompanyName = SeedCompanies.DefaultCompanyName }
            );

            builder.HasQueryFilter(u => !u.IsDeleted);   //for soft Delete
        }
    }
}
