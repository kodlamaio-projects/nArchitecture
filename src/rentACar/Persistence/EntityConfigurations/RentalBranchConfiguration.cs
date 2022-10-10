using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations
{
    public class RentalBranchConfiguration : IEntityTypeConfiguration<RentalBranch>
    {
        public void Configure(EntityTypeBuilder<RentalBranch> builder)
        {
            builder.ToTable("RentalBranches").HasKey(r => r.Id);
            builder.Property(r => r.Id).HasColumnName("Id");
            builder.Property(r => r.City).HasColumnName("City");
            builder.HasMany(r => r.Cars);

            RentalBranch[] rentalBranchSeeds = { new(1, City.Ankara), new(2, City.Antalya) };
            builder.HasData(rentalBranchSeeds);
        }
    }
}
