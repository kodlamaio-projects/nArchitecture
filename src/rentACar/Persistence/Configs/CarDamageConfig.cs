using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configs;

public sealed class CarDamageConfig : IEntityTypeConfiguration<CarDamage>
{
    public void Configure(EntityTypeBuilder<CarDamage> builder)
    {
        builder.ToTable("CarDamages").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.CarId).HasColumnName("CarId");
        builder.Property(p => p.IsFixed).HasColumnName("IsFixed").HasDefaultValue(false);
        builder.HasOne(p => p.Car);


    }
}
