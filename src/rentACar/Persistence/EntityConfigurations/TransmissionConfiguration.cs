using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class TransmissionConfiguration : IEntityTypeConfiguration<Transmission>
{
    public void Configure(EntityTypeBuilder<Transmission> builder)
    {
        builder.ToTable("Transmissions").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.Name).HasColumnName("Name");
        builder.HasIndex(indexExpression: p => p.Name, name: "UK_Transmissions_Name").IsUnique();
        builder.HasMany(p => p.Models);

        Transmission[] transmissionsSeeds = { new(id: 1, name: "Manuel"), new(id: 2, name: "Automatic") };
        builder.HasData(transmissionsSeeds);
    }
}
