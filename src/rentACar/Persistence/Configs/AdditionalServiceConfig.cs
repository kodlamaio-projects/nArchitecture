using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configs;

public sealed class AdditionalServiceConfig : IEntityTypeConfiguration<AdditionalService>
{
    public void Configure(EntityTypeBuilder<AdditionalService> builder)
    {
        builder.ToTable("AdditionalServices").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.Name).HasColumnName("Name");
        builder.Property(p => p.DailyPrice).HasColumnName("DailyPrice");

        AdditionalService[] additionalServiceSeeds = { new(1, "Baby Seat", 200), new(2, "Scooter", 300) };
        builder.HasData(additionalServiceSeeds);
    }
}
