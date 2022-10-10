using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations
{
    public class OtpAuthenticatorConfiguration : IEntityTypeConfiguration<OtpAuthenticator>
    {
        public void Configure(EntityTypeBuilder<OtpAuthenticator> builder)
        {
            builder.ToTable("OtpAuthenticators").HasKey(e => e.Id);
            builder.Property(e => e.UserId).HasColumnName("UserId");
            builder.Property(e => e.SecretKey).HasColumnName("SecretKey");
            builder.Property(e => e.IsVerified).HasColumnName("IsVerified");
            builder.HasOne(e => e.User);
        }
    }
}
