using PluralizeService.Core;

namespace Generator.Codes.CreatorCodes.Feature;

internal class ConstantsCreator : ICreatorCode
{
    public ConstantsCreator(Type type)
    {
        Type = type;
    }

    public Type Type { get; set; }

    public string ConstantsCreate()
    {

        string plural = PluralizationProvider.Pluralize(Type.Name);
        return
            $@"namespace Application.Features.{plural}.Constants;

public static class {Type.Name}Messages
{{
}}";
    }
    public string EntityOperationClaimsCreate()
    {

        string plural = PluralizationProvider.Pluralize(Type.Name);
        return
            $@"namespace Application.Features.{plural}.Constants;

public static class {Type.Name}OperationClaims
{{
}}";
    }
}
