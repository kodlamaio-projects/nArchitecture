using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class FuelConfiguration : IEntityTypeConfiguration<Fuel>
{
    public void Configure(EntityTypeBuilder<Fuel> builder)
    {
        builder.ToTable("Fuels").HasKey(f => f.Id);
        builder.Property(f => f.Id).HasColumnName("Id");
        builder.Property(f => f.Name).HasColumnName("Name");
        builder.HasIndex(indexExpression: f => f.Name, name: "UK_Fuels_Name").IsUnique();
        builder.HasMany(f => f.Models);

        Fuel[] fuelSeeds = { new(id: 1, name: "Diesel"), new(id: 2, name: "Electric") };
        builder.HasData(fuelSeeds);
    }
}
