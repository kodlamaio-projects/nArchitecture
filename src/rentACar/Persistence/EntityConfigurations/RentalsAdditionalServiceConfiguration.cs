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
    public class RentalsAdditionalServiceConfiguration : IEntityTypeConfiguration<RentalsAdditionalService>
    {
        public void Configure(EntityTypeBuilder<RentalsAdditionalService> builder)
        {
            builder.ToTable("RentalsAdditionalServices").HasKey(r => r.Id);

            builder.Property(r => r.Id).HasColumnName("Id");

            builder.Property(r => r.RentalId).HasColumnName("RentalId");

            builder.Property(r => r.AdditionalServiceId).HasColumnName("AdditionalServiceId");

            builder.HasOne(r => r.Rental);

            builder.HasOne(r => r.AdditionalService);

        }
    }
}
