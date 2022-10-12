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
    public class FindeksCreditRateConfiguration : IEntityTypeConfiguration<FindeksCreditRate>
    {
        public void Configure(EntityTypeBuilder<FindeksCreditRate> builder)
        {
            builder.ToTable("FindeksCreditRates").HasKey(f => f.Id);
            builder.Property(f => f.Id).HasColumnName("Id");
            builder.Property(f => f.CustomerId).HasColumnName("CustomerId");
            builder.Property(f => f.Score).HasColumnName("Score");
            builder.HasOne(f => f.Customer);

            FindeksCreditRate[] findeksCreditRates = { new(1, 1, 1000), new(2, 2, 1900) };
            builder.HasData(findeksCreditRates);
        }
    }
}
