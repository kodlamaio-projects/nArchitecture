using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configs;

public sealed class CorporateCustomer : IEntityTypeConfiguration<Domain.Entities.CorporateCustomer>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.CorporateCustomer> builder)
    {
        builder.ToTable("CorporateCustomers").HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("Id");
        builder.Property(c => c.CustomerId).HasColumnName("CustomerId");
        builder.Property(c => c.CompanyName).HasColumnName("CompanyName");
        builder.Property(c => c.TaxNo).HasColumnName("TaxNo");
        builder.HasOne(c => c.Customer);

        Domain.Entities.CorporateCustomer[] corporateCustomers = { new(1, 2, "Ahmet Çetinkaya", "54154512") };
        builder.HasData(corporateCustomers);
    }
}
