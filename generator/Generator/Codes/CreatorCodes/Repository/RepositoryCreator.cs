namespace Generator.Codes.CreatorCodes.Repository;

internal class RepositoryCreator : ICreatorCode
{
    public RepositoryCreator(Type type)
    {
        Type = type;
    }

    public Type Type { get; set; }

    public string ISyncAndAsyncRepository()
    {
        return
$@"// this file was created automatically.
using Core.Persistence.Repositories;
using {Type.Namespace};

namespace Application.Repositories;

public interface I{Type.Name}Repository : IAsyncRepository<{Type.Name}>, IRepository<{Type.Name}>
{{
}}
";
    }

    public string ISyncRepository()
    {

        return
$@"// this file was created automatically.
using Core.Persistence.Repositories;
using {Type.Namespace};

namespace Application.Repositories;

public interface I{Type.Name}Repository : IRepository<{Type.Name}>
{{
}}
";
    }

    public string IAsyncRepository()
    {
        return
$@"// this file was created automatically.
using Core.Persistence.Repositories;
using {Type.Namespace};

namespace Application.Repositories;

public interface I{Type.Name}Repository : IAsyncRepository<{Type.Name}>
{{
}}
";

    }

    public string IEmptyRepository()
    {
        return
$@"// this file was created automatically.
using Core.Persistence.Repositories;
using {Type.Namespace};

namespace Application.Repositories;

public interface I{Type.Name}Repository
{{
}}
";
    }

    public string Repository(string dBContext)
    {
        return
$@"// this file was created automatically.
using Core.Persistence.Repositories;
using {Type.Namespace};
using Persistence.Contexts;
using Application.Repositories;

namespace Persistence.Repositories;

public class {Type.Name}Repository : EfRepositoryBase<{Type.Name}, {dBContext}>, I{Type.Name}Repository
{{
    public {Type.Name}Repository({dBContext} context) : base(context) {{ }}
}}
";
    }

}