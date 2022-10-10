using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configs;

public sealed class FindeksCreditRateConfig : IEntityTypeConfiguration<FindeksCreditRate>
{
    public void Configure(EntityTypeBuilder<FindeksCreditRate> builder)
    {
        builder.ToTable("FindeksCreditRates").HasKey(f => f.Id);
        builder.Property(f => f.Id).HasColumnName("Id");
        builder.Property(f => f.CustomerId).HasColumnName("CustomerId");
        builder.Property(f => f.Score).HasColumnName("Score");
        builder.HasOne(f => f.Customer);

        FindeksCreditRate[] findeksCreditRates = { new(1, 1, 1000), new(2, 2, 1900) };
        builder.HasData(findeksCreditRates);
    }
}
