using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ModelConfiguration : IEntityTypeConfiguration<Model>
{
    public void Configure(EntityTypeBuilder<Model> builder)
    {
        builder.ToTable("Models").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.BrandId).HasColumnName("BrandId");
        builder.Property(p => p.FuelId).HasColumnName("FuelId");
        builder.Property(p => p.TransmissionId).HasColumnName("TransmissionId");
        builder.Property(p => p.Name).HasColumnName("Name");
        builder.HasIndex(indexExpression: p => p.Name, name: "UK_Models_Name").IsUnique();
        builder.Property(p => p.DailyPrice).HasColumnName("DailyPrice");
        builder.Property(p => p.ImageUrl).HasColumnName("ImageUrl");
        builder.HasOne(p => p.Brand);
        builder.HasMany(p => p.Cars);
        builder.HasOne(p => p.Fuel);
        builder.HasOne(p => p.Transmission);

        Model[] modelSeeds =
        {
            new(id: 1, brandId: 1, fuelId: 1, transmissionId: 2, name: "418i", dailyPrice: 1000, imageUrl: ""),
            new(id: 2, brandId: 2, fuelId: 2, transmissionId: 1, name: "CLA 180D", dailyPrice: 600, imageUrl: "")
        };
        builder.HasData(modelSeeds);
    }
}
