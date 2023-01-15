using PluralizeService.Core;

namespace Generator.Codes.CreatorCodes.Feature;

internal class ModelCreator : ICreatorCode
{
    public ModelCreator(Type type)
    {
        Type = type;
    }

    public Type Type { get; set; }

    public string ListModelCreate()
    {
        string plural = PluralizationProvider.Pluralize(Type.Name);

        return
            @$"// this file was created automatically.
using Application.Features.{plural}.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.{plural}.Models;

public class {Type.Name}ListModel : BasePageableModel
{{
    public IList<{Type.Name}ListDto> Items {{ get; set; }}
}}
";
    }
}
