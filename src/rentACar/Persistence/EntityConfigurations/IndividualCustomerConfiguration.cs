using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class IndividualCustomerConfiguration : IEntityTypeConfiguration<IndividualCustomer>
{
    public void Configure(EntityTypeBuilder<IndividualCustomer> builder)
    {
        builder.ToTable("IndividualCustomers").HasKey(i => i.Id);
        builder.Property(i => i.Id).HasColumnName("Id");
        builder.Property(i => i.CustomerId).HasColumnName("CustomerId");
        builder.HasIndex(indexExpression: i => i.CustomerId, name: "UK_IndividualCustomers_CustomerId").IsUnique();
        builder.Property(i => i.FirstName).HasColumnName("FirstName");
        builder.Property(i => i.LastName).HasColumnName("LastName");
        builder.Property(i => i.NationalIdentity).HasColumnName("NationalIdentity");
        builder.HasIndex(indexExpression: i => i.NationalIdentity, name: "UK_IndividualCustomers_NationalIdentity").IsUnique();
        builder.HasOne(i => i.Customer);
    }
}
