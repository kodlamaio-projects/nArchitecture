using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Domain.Constants.OperationClaims;

namespace Persistence.EntityConfigurations
{
    public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
    {
        public void Configure(EntityTypeBuilder<OperationClaim> builder)
        {
            builder.ToTable("OperationClaims").HasKey(o => o.Id);
            builder.Property(o => o.Id).HasColumnName("Id");
            builder.Property(o => o.Name).HasColumnName("Name");
            builder.HasIndex(o => o.Name, "UK_OperationClaims_Name").IsUnique();

            OperationClaim[] operationClaimSeeds = { new(1, Admin) };
            builder.HasData(operationClaimSeeds);
        }
    }
}
