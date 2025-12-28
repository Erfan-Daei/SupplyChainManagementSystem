using Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DatabaseManagement.EntitiesConfiguration.UserManagementConfiguration
{
    public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.HasKey(ut => ut.UserTokenId);

            builder.Property(ut => ut.UserTokenValue)
                .IsRequired()
                .HasMaxLength(60)
                .IsUnicode(false);

            builder.Property(ut => ut.UserTokenType)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(ut => ut.UserTokenExpireTime)
                .IsRequired()
                .HasDefaultValue(DateTime.UtcNow.AddMinutes(10));

            builder.Property(ut => ut.UserTokenIsExpired)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(ut => ut.UserTokenIsUsed)
                .IsRequired()
                .HasDefaultValue(false);

            // 1 User to many UserTokenRelation
            builder.HasOne(ut => ut.User)
                .WithMany(u => u.UserTokens)
                .HasForeignKey(ut => ut.UserId)
                .OnDelete(DeleteBehavior.NoAction);   //to avoid delete
        }
    }
}
