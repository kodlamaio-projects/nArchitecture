using Application.Features.OperationClaims.Constants;
using Application.Features.Users.Constants;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;

namespace Persistence.EntityConfigurations;

public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.ToTable("OperationClaims").HasKey(oc => oc.Id);

        builder.Property(oc => oc.Id).HasColumnName("Id").IsRequired();
        builder.Property(oc => oc.Name).HasColumnName("Name").IsRequired();
        builder.Property(oc => oc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(oc => oc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(oc => oc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(oc => !oc.DeletedDate.HasValue);

        builder.HasMany(oc => oc.UserOperationClaims);

        builder.HasData(getSeeds());
    }

    private List<OperationClaim> getSeeds()
    {
        int id = 0;
        List<OperationClaim> seeds =
            new()
            {
                new OperationClaim {
                    Id = ++id,
                    Name = GeneralOperationClaims.Admin
                }
            };

        var operationClaims = setApplicationOperationClaims(id,
             typeof(UsersOperationClaims).GetFields()
             );

        seeds.AddRange(operationClaims);
        return seeds;
    }

    private List<OperationClaim> setApplicationOperationClaims(int id, params FieldInfo[] objects)
    {
        List<OperationClaim> list = new List<OperationClaim>();
        foreach (var item in objects)
        {
            list.Add(new OperationClaim
            {
                Id = ++id,
                Name = item.GetValue(null)?.ToString() ?? ""
            });
        }
        return list;
    }
}
