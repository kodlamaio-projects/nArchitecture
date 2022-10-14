using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations
{
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
            builder.HasIndex(p => p.Name, "UK_Models_Name").IsUnique();
            builder.Property(p => p.DailyPrice).HasColumnName("DailyPrice");
            builder.Property(p => p.ImageUrl).HasColumnName("ImageUrl");
            builder.HasOne(p => p.Brand);
            builder.HasMany(p => p.Cars);
            builder.HasOne(p => p.Fuel);
            builder.HasOne(p => p.Transmission);

            Model[] modelSeeds = { new(1, 1, 1, 2, "418i", 1000, ""), new(2, 2, 2, 1, "CLA 180D", 600, "") };
            builder.HasData(modelSeeds);
        }
    }
}
