namespace Generator.Codes.CreatorCodes.Service;

internal class AltServiceCreator : ICreatorCode
{
    public AltServiceCreator(Type type)
    {
        Type = type;
    }

    public Type Type { get; set; }

    public string IServiceCreate()
    {
        return
            $@"// this file was created automatically.
namespace Application.Services.{Type.Name}Service;

public interface I{Type.Name}Service
{{
}}
";
    }

    public string ServiceCreate()
    {
        return
            $@"// this file was created automatically.
namespace Application.Services.{Type.Name}Service;

public class {Type.Name}Manager : I{Type.Name}Service
{{
}}
";
    }
}
