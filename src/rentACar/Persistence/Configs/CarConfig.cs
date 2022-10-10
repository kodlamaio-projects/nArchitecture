using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configs;

public sealed class CarConfig : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable("Cars").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.ColorId).HasColumnName("ColorId");
        builder.Property(p => p.ModelId).HasColumnName("ModelId");
        builder.Property(p => p.RentalBranchId).HasColumnName("RentalBranchId");
        builder.Property(p => p.Kilometer).HasColumnName("Kilometer");
        builder.Property(p => p.CarState).HasColumnName("State");
        builder.Property(p => p.ModelYear).HasColumnName("ModelYear");
        builder.Property(p => p.Plate).HasColumnName("Plate");
        builder.HasOne(p => p.Color);
        builder.HasMany(p => p.CarDamages);
        builder.HasOne(p => p.Model);
        builder.HasOne(c => c.RentalBranch);

        Car[] carSeeds =
        {
            new(1, 1, 1, 1, CarState.Available, 1000, 2018, "07ABC07", 500),
            new(2, 2, 2, 2, CarState.Rented, 1000, 2018, "15ABC15", 1100)
        };
        builder.HasData(carSeeds);
    }
}
