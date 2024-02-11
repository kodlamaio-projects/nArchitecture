using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NArchitecture.Core.Security.Entities;

namespace Persistence.EntityConfigurations;

public class EmailAuthenticatorConfiguration : IEntityTypeConfiguration<EmailAuthenticator<int, int>>
{
    public void Configure(EntityTypeBuilder<EmailAuthenticator<int, int>> builder)
    {
        builder.ToTable("EmailAuthenticators").HasKey(ea => ea.Id);

        builder.Property(ea => ea.Id).HasColumnName("Id").IsRequired();
        builder.Property(ea => ea.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(ea => ea.ActivationKey).HasColumnName("ActivationKey");
        builder.Property(ea => ea.IsVerified).HasColumnName("IsVerified").IsRequired();
        builder.Property(ea => ea.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ea => ea.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ea => ea.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ea => !ea.DeletedDate.HasValue);

        builder.HasOne(ea => ea.User);
    }
}
