using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations
{
    public class IndividualCustomerConfiguration : IEntityTypeConfiguration<IndividualCustomer>
    {
        public void Configure(EntityTypeBuilder<IndividualCustomer> builder)
        {
            builder.ToTable("IndividualCustomers").HasKey(i => i.Id);
            builder.Property(i => i.Id).HasColumnName("Id");
            builder.Property(i => i.CustomerId).HasColumnName("CustomerId");
            builder.HasIndex(i => i.CustomerId, "UK_IndividualCustomers_CustomerId").IsUnique();
            builder.Property(i => i.FirstName).HasColumnName("FirstName");
            builder.Property(i => i.LastName).HasColumnName("LastName");
            builder.Property(i => i.NationalIdentity).HasColumnName("NationalIdentity");
            builder.HasIndex(i => i.NationalIdentity, "UK_IndividualCustomers_NationalIdentity").IsUnique();
            builder.HasOne(i => i.Customer);
        }
    }
}
