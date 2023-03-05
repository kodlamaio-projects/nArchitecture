using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class AdditionalServiceConfiguration : IEntityTypeConfiguration<AdditionalService>
{
    public void Configure(EntityTypeBuilder<AdditionalService> builder)
    {
        builder.ToTable("AdditionalServices").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.Name).HasColumnName("Name");
        builder.HasIndex(indexExpression: p => p.Name, name: "UK_AdditionalServices_Name").IsUnique();
        builder.Property(p => p.DailyPrice).HasColumnName("DailyPrice");

        AdditionalService[] additionalServiceSeeds =
        {
            new(id: 1, name: "Baby Seat", dailyPrice: 200),
            new(id: 2, name: "Scooter", dailyPrice: 300)
        };
        builder.HasData(additionalServiceSeeds);
    }
}
