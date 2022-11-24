using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            builder.Property(r => r.RentStartRentalBranchId).HasColumnName("RentStartRentalBranchId");
            builder.Property(r => r.RentEndRentalBranchId).HasColumnName("RentEndRentalBranchId");
            builder.Property(r => r.RentStartDate).HasColumnName("RentStartDate");
            builder.Property(r => r.RentEndDate).HasColumnName("RentEndDate");
            builder.Property(r => r.ReturnDate).HasColumnName("ReturnDate");
            builder.Property(r => r.RentStartKilometer).HasColumnName("RentStartKilometer");
            builder.Property(r => r.RentEndKilometer).HasColumnName("RentEndKilometer");
            builder.HasOne(r => r.Car);
            builder.HasOne(r => r.Customer);
            builder.HasOne(r => r.RentStartRentalBranch).WithOne().HasForeignKey<Rental>(r=>r.RentStartRentalBranchId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(r => r.RentEndRentalBranch).WithOne().HasForeignKey<Rental>(r=>r.RentEndRentalBranchId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(r => r.RentalsAdditionalServices);
        }
    }
}
