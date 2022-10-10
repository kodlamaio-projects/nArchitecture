using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations
{
    public class EmailAuthenticatorConfiguration : IEntityTypeConfiguration<EmailAuthenticator>
    {
        public void Configure(EntityTypeBuilder<EmailAuthenticator> builder)
        {
            builder.ToTable("EmailAuthenticators").HasKey(e => e.Id);
            builder.Property(e => e.UserId).HasColumnName("UserId");
            builder.Property(e => e.ActivationKey).HasColumnName("ActivationKey");
            builder.Property(e => e.IsVerified).HasColumnName("IsVerified");
            builder.HasOne(e => e.User);
        }
    }
}
