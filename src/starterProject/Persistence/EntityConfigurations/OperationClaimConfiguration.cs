using System.Reflection;
using Application;
using Core.Security.Attributes;
using Core.Security.Constants;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim<int, int>>
{
    public void Configure(EntityTypeBuilder<OperationClaim<int, int>> builder)
    {
        builder.ToTable("OperationClaims").HasKey(oc => oc.Id);

        builder.Property(oc => oc.Id).HasColumnName("Id").IsRequired();
        builder.Property(oc => oc.Name).HasColumnName("Name").IsRequired();
        builder.Property(oc => oc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(oc => oc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(oc => oc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(oc => !oc.DeletedDate.HasValue);

        builder.HasMany(oc => oc.UserOperationClaims);

        builder.HasData(_seeds);
    }

    private IEnumerable<OperationClaim<int, int>> _seeds
    {
        get
        {
            int id = 0;

            yield return new() { Id = ++id, Name = GeneralOperationClaims.Admin };

            IEnumerable<OperationClaim<int, int>> featureOperationClaims = getFeatureOperationClaims(id);
            foreach (OperationClaim<int, int> claim in featureOperationClaims)
                yield return claim;
            //id += featureOperationClaims.Count();
        }
    }

    private IEnumerable<OperationClaim<int, int>> getFeatureOperationClaims(int initialId)
    {
        int id = initialId;
        IEnumerable<Type> featureOperationClaimsTypes = Assembly
            .GetAssembly(typeof(ApplicationServiceRegistration))!
            .GetTypes()
            .Where(type => type.GetCustomAttributes(typeof(OperationClaimConstantsAttribute)).Any() && type.IsClass);
        foreach (Type type in featureOperationClaimsTypes)
        {
            FieldInfo[] typeFields = type.GetFields(BindingFlags.Public | BindingFlags.Static);
            IEnumerable<string> typeFieldsValues = typeFields.Select(field => field.GetValue(null)!.ToString()!);

            foreach (string value in typeFieldsValues)
                yield return new() { Id = ++id, Name = value };
        }
    }
}
