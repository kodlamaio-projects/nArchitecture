using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configs;

public sealed class InvoiceConfig : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoices").HasKey(i => i.Id);
        builder.Property(i => i.Id).HasColumnName("Id");
        builder.Property(i => i.CustomerId).HasColumnName("CustomerId");
        builder.Property(i => i.No).HasColumnName("No");
        builder.Property(i => i.CreatedDate).HasColumnName("CreatedDate").HasDefaultValue(DateTime.Now);
        builder.Property(i => i.RentalStartDate).HasColumnName("RentalStartDate");
        builder.Property(i => i.RentalEndDate).HasColumnName("RentalEndDate");
        builder.Property(i => i.TotalRentalDate).HasColumnName("TotalRentalDate");
        builder.Property(i => i.RentalPrice).HasColumnName("RentalPrice");
        builder.HasOne(i => i.Customer);

        Invoice[] invoiceSeeds =
        {
            new(1, 1, "123123", DateTime.Today, DateTime.Today, DateTime.Today.AddDays(2), 2, 1000),
            new(2, 1, "123123", DateTime.Today, DateTime.Today, DateTime.Today.AddDays(2), 2, 2000)
        };
        builder.HasData(invoiceSeeds);
    }
}
