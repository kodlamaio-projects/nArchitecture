using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations
{
    public class CorporateCustomerConfiguration : IEntityTypeConfiguration<CorporateCustomer>
    {
        public void Configure(EntityTypeBuilder<CorporateCustomer> builder)
        {
            builder.ToTable("CorporateCustomers").HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("Id");
            builder.Property(c => c.CustomerId).HasColumnName("CustomerId");
            builder.HasIndex(c => c.CustomerId, "UK_CorporateCustomers_CustomerId").IsUnique();
            builder.Property(c => c.CompanyName).HasColumnName("CompanyName");
            builder.Property(c => c.TaxNo).HasColumnName("TaxNo");
            builder.HasIndex(c => c.TaxNo, "UK_CorporateCustomers_TaxNo").IsUnique();
            builder.HasOne(c => c.Customer);
        }
    }
}
