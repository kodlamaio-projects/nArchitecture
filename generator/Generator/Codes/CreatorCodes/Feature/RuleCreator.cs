using PluralizeService.Core;

namespace Generator.Codes.CreatorCodes.Feature;

internal class RuleCreator : ICreatorCode
{
    public RuleCreator(Type type)
    {
        Type = type;
    }

    public Type Type { get; set; }

    public string RuleCreate()
    {
        string plural = PluralizationProvider.Pluralize(Type.Name);
        return
            $@"// this file was created automatically.
using Application.Repositories;
using Application.Features.{plural}.Constants;
using Core.Application.Rules;
using {Type.Namespace};

namespace Application.Features.{plural}.Rules;

public class {Type.Name}BusinessRules : BaseBusinessRules
{{

    private readonly I{Type.Name}Repository _{Type.Name.ToLower()}Repository;

    public {Type.Name}BusinessRules(I{Type.Name}Repository {Type.Name.ToLower()}Repository)
    {{
        _{Type.Name.ToLower()}Repository = {Type.Name.ToLower()}Repository;
    }}
}}
";
    }
}
