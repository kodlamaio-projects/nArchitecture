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
    public class UserOperationClaimConfiguration : IEntityTypeConfiguration<UserOperationClaim>
    {
        public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
        {
            builder.ToTable("UserOperationClaims").HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("Id");
            builder.Property(u => u.UserId).HasColumnName("UserId");
            builder.Property(u => u.OperationClaimId).HasColumnName("OperationClaimId");
            builder.HasIndex(u => new { u.UserId, u.OperationClaimId }, "UK_UserOperationClaims_UserId_OperationClaimId").IsUnique();
            builder.HasOne(u => u.User);
            builder.HasOne(u => u.OperationClaim);
        }
    }
}
