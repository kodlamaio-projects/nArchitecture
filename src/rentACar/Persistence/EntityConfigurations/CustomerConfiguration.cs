using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers").HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("Id");
        builder.Property(c => c.UserId).HasColumnName("UserId");
        builder.HasIndex(indexExpression: c => c.UserId, name: "UK_Customers_UserId").IsUnique();
        builder.HasOne(c => c.User);
        builder.HasOne(c => c.CorporateCustomer);
        builder.HasOne(c => c.FindeksCreditRate);
        builder.HasOne(c => c.IndividualCustomer);
        builder.HasMany(c => c.Invoices);
        builder.HasMany(c => c.Rentals);
    }
}
