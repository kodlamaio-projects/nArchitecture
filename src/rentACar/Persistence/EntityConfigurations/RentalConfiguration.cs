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
    public class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.ToTable("Rentals").HasKey(k => k.Id);
            builder.Property(r => r.Id).HasColumnName("Id");
            builder.Property(r => r.CustomerId).HasColumnName("CustomerId");
            builder.Property(r => r.CarId).HasColumnName("CarId");
            //builder.Property(r => r.RentStartRentalBranchId).HasColumnName("RentStartRentalBranchId");
            //builder.Property(r => r.RentEndRentalBranchId).HasColumnName("RentEndRentalBranchId");
            builder.Property(r => r.RentStartDate).HasColumnName("RentStartDate");
            builder.Property(r => r.RentEndDate).HasColumnName("RentEndDate");
            builder.Property(r => r.ReturnDate).HasColumnName("ReturnDate");
            builder.Property(r => r.RentStartKilometer).HasColumnName("RentStartKilometer");
            builder.Property(r => r.RentEndKilometer).HasColumnName("RentEndKilometer");
            builder.HasOne(r => r.Car);
            //builder.HasOne(r => r.Customer);
            builder.HasOne(r => r.RentStartRentalBranch).WithMany().HasForeignKey(r=>r.RentStartRentalBranchId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(r => r.RentEndRentalBranch).WithMany().HasForeignKey(r => r.RentEndRentalBranchId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(r => r.RentalsAdditionalServices);

            Rental[] rentalSeeds =
            {
                new(1, 1, 2, 1, 2, DateTime.Today, DateTime.Today.AddDays(2), null, 1000, 1200),
                new(2, 2, 1, 2, 1, DateTime.Today, DateTime.Today.AddDays(2), null, 1000, 1200)
            };
            builder.HasData(rentalSeeds);
        }
    }
}
