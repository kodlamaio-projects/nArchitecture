using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ColorConfiguration : IEntityTypeConfiguration<Color>
{
    public void Configure(EntityTypeBuilder<Color> builder)
    {
        builder.ToTable("Colors").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.Name).HasColumnName("Name");
        builder.HasIndex(indexExpression: p => p.Name, name: "UK_Colors_Name").IsUnique();
        builder.HasMany(p => p.Cars);

        Color[] colorSeeds = { new(id: 1, name: "Red"), new(id: 2, name: "Blue") };
        builder.HasData(colorSeeds);
    }
}
