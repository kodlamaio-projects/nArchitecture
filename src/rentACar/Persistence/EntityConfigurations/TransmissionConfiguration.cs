using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations
{
    public class TransmissionConfiguration : IEntityTypeConfiguration<Transmission>
    {
        public void Configure(EntityTypeBuilder<Transmission> builder)
        {
            builder.ToTable("Transmissions").HasKey(k => k.Id);
            builder.Property(p => p.Id).HasColumnName("Id");
            builder.Property(p => p.Name).HasColumnName("Name");
            builder.HasIndex(p => p.Name, "UK_Transmissions_Name").IsUnique();
            builder.HasMany(p => p.Models);

            Transmission[] transmissionsSeeds = { new(1, "Manuel"), new(2, "Automatic") };
            builder.HasData(transmissionsSeeds);
        }
    }
}
