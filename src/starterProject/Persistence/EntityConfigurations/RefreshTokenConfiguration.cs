using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken<int, int>>
{
    public void Configure(EntityTypeBuilder<RefreshToken<int, int>> builder)
    {
        builder.ToTable("RefreshTokens").HasKey(rt => rt.Id);

        builder.Property(rt => rt.Id).HasColumnName("Id").IsRequired();
        builder.Property(rt => rt.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(rt => rt.Token).HasColumnName("Token").IsRequired();
        builder.Property(rt => rt.ExpiresDate).HasColumnName("ExpiresDate").IsRequired();
        builder.Property(rt => rt.CreatedByIp).HasColumnName("CreatedByIp").IsRequired();
        builder.Property(rt => rt.RevokedDate).HasColumnName("RevokedDate");
        builder.Property(rt => rt.RevokedByIp).HasColumnName("RevokedByIp");
        builder.Property(rt => rt.ReplacedByToken).HasColumnName("ReplacedByToken");
        builder.Property(rt => rt.ReasonRevoked).HasColumnName("ReasonRevoked");
        builder.Property(rt => rt.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(rt => rt.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(rt => rt.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(rt => !rt.DeletedDate.HasValue);

        builder.HasOne(rt => rt.User);
    }
}
