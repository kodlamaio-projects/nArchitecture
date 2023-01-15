using PluralizeService.Core;

namespace Generator.Codes.CreatorCodes.Feature;

internal class ProfileCreator : ICreatorCode
{
    public ProfileCreator(Type type)
    {
        Type = type;
    }

    public Type Type { get; set; }

    public string MappingProfileCreator()
    {
        return
            $@"// this file was created automatically.
using AutoMapper;
using {Type.Namespace};

namespace Application.Features.{PluralizationProvider.Pluralize(Type.Name)}.Profiles;

public class MappingProfiles : Profile
{{
    public MappingProfiles()
    {{
    
    }}
}}
";
    }
}
