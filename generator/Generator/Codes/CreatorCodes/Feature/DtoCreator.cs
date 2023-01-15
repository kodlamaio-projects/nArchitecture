using PluralizeService.Core;

namespace Generator.Codes.CreatorCodes.Feature;

internal class DtoCreator : ICreatorCode
{
    public DtoCreator(Type type)
    {
        Type = type;
    }

    public Type Type { get; set; }

    public string DtoCreate()
    {
        string plural = PluralizationProvider.Pluralize(Type.Name);
        return

$@"// this file was created automatically.

namespace Application.Features.{plural}.Dtos;

public class {Type.Name}Dto
{{
}}
";
    }
    public string ListDtoCreate()
    {
        string plural = PluralizationProvider.Pluralize(Type.Name);
        return

$@"// this file was created automatically.

namespace Application.Features.{plural}.Dtos;

public class {Type.Name}ListDto
{{
}}
";
    }
    public string CreatedDtoCreate()
    {
        string plural = PluralizationProvider.Pluralize(Type.Name);
        return

$@"// this file was created automatically.

namespace Application.Features.{plural}.Dtos;

public class Created{Type.Name}Dto
{{
}}
";
    }
    public string DeletedDtoCreate()
    {
        string plural = PluralizationProvider.Pluralize(Type.Name);
        return

$@"// this file was created automatically.

namespace Application.Features.{plural}.Dtos;

public class Deleted{Type.Name}Dto
{{
}}
";
    }
    public string UpdatedDtoCreate()
    {
        string plural = PluralizationProvider.Pluralize(Type.Name);
        return

$@"// this file was created automatically.

namespace Application.Features.{plural}.Dtos;

public class Updated{Type.Name}Dto
{{
}}
";
    }
}
