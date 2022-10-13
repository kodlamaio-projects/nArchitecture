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
    public class AdditionalServiceConfiguration : IEntityTypeConfiguration<AdditionalService>
    {
        public void Configure(EntityTypeBuilder<AdditionalService> builder)
        {
            builder.ToTable("AdditionalServices").HasKey(k => k.Id);
            builder.Property(p => p.Id).HasColumnName("Id");
            builder.Property(p => p.Name).HasColumnName("Name");
            builder.HasIndex(p => p.Name, "UK_AdditionalServices_Name").IsUnique();
            builder.Property(p => p.DailyPrice).HasColumnName("DailyPrice");

            AdditionalService[] additionalServiceSeeds = { new(1, "Baby Seat", 200), new(2, "Scooter", 300) };
            builder.HasData(additionalServiceSeeds);
        }
    }
}
