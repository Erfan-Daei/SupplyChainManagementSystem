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

            builder.HasQueryFilter(u => !u.IsDeleted);   //for soft Delete
        }
    }
}
