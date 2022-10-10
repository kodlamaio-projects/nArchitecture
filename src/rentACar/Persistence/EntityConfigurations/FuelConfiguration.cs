﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations
{
    public class FuelConfiguration : IEntityTypeConfiguration<Fuel>
    {
        public void Configure(EntityTypeBuilder<Fuel> builder)
        {
            builder.ToTable("Fuels").HasKey(f => f.Id);
            builder.Property(f => f.Id).HasColumnName("Id");
            builder.Property(f => f.Name).HasColumnName("Name");
            builder.HasMany(f => f.Models);

            Fuel[] fuelSeeds = { new(1, "Diesel"), new(2, "Electric") };
            builder.HasData(fuelSeeds);
        }
    }
}
