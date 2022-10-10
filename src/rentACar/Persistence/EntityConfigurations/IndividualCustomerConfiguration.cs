using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations
{
    public class IndividualCustomerConfiguration : IEntityTypeConfiguration<IndividualCustomer>
    {
        public void Configure(EntityTypeBuilder<IndividualCustomer> builder)
        {
            builder.ToTable("IndividualCustomers").HasKey(i => i.Id);
            builder.Property(i => i.Id).HasColumnName("Id");
            builder.Property(i => i.CustomerId).HasColumnName("CustomerId");
            builder.Property(i => i.FirstName).HasColumnName("FirstName");
            builder.Property(i => i.LastName).HasColumnName("LastName");
            builder.Property(i => i.NationalIdentity).HasColumnName("NationalIdentity");
            builder.HasOne(i => i.Customer);

            IndividualCustomer[] individualCustomers = { new(1, 1, "Ahmet", "Çetinkaya", "123123123123") };
            builder.HasData(individualCustomers);
        }
    }
}
