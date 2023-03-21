using Application.Features.OperationClaims.Constants;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.ToTable("OperationClaims").HasKey(o => o.Id);
        builder.Property(o => o.Id).HasColumnName("Id");
        builder.Property(o => o.Name).HasColumnName("Name");
        builder.HasIndex(indexExpression: o => o.Name, name: "UK_OperationClaims_Name").IsUnique();

        builder.HasData(getSeeds());
    }

    private HashSet<OperationClaim> getSeeds()
    {
        int id = 0;
        HashSet<OperationClaim> seeds = new();

        seeds.Add(new OperationClaim { Id = ++id, Name = GeneralOperationClaims.Admin });

        return seeds;
    }
}
