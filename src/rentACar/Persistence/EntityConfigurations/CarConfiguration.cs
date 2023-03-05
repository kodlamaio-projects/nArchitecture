using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable("Cars").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.ColorId).HasColumnName("ColorId");
        builder.Property(p => p.ModelId).HasColumnName("ModelId");
        builder.Property(c => c.RentalBranchId).HasColumnName("RentalBranchId");
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
            new(
                id: 1,
                colorId: 1,
                modelId: 1,
                rentalBranchId: 1,
                CarState.Available,
                kilometer: 1000,
                modelYear: 2018,
                plate: "07ABC07",
                minFindeksCreditRate: 500
            ),
            new(
                id: 2,
                colorId: 2,
                modelId: 2,
                rentalBranchId: 2,
                CarState.Rented,
                kilometer: 1000,
                modelYear: 2018,
                plate: "15ABC15",
                minFindeksCreditRate: 1100
            )
        };
        builder.HasData(carSeeds);
    }
}
