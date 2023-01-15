using Generator.Codes.CreatorCodes.Feature;
using Generator.Codes.CreatorCodes.Repository;
using Generator.Codes.CreatorCodes.Service;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Generator.Codes.CreatorCodes;

internal static class CsFileOperation
{
    static CsFile GetNamespacesPathsAndFileNames(string fileContent)
    {

        SyntaxTree tree = CSharpSyntaxTree.ParseText(fileContent);
        CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

        NamespaceDeclarationSyntax? namespaceNode = root.DescendantNodes().OfType<NamespaceDeclarationSyntax>().FirstOrDefault();
        ClassDeclarationSyntax? classNode = root.DescendantNodes().OfType<ClassDeclarationSyntax>().FirstOrDefault();
        InterfaceDeclarationSyntax? interfaceNode = root.DescendantNodes().OfType<InterfaceDeclarationSyntax>().FirstOrDefault();

        string path = namespaceNode.Name.ToString().Replace('.', '\\') ?? "";

        string fileName = classNode?.Identifier.ValueText ?? interfaceNode?.Identifier.ValueText ?? "";


        return new()
        {
            FileName = fileName,
            Path = path,
            FileContent = fileContent
        };
    }

    public static IList<CsFile> CsFilesEngine(IList<Type> types, CsFileOperationConfig csFileOperationConfig)
    {
        List<CsFile> csFiles = new();

        foreach (Type type in types)
        {
            foreach (string csFileContent in CsFileContentGenerator(type, csFileOperationConfig))
            {
                csFiles.Add(GetNamespacesPathsAndFileNames(csFileContent));
            }
        }

        return csFiles;
    }

    static IList<string> CsFileContentGenerator(Type type, CsFileOperationConfig csFileOperationConfig)
    {
        List<string> fileContents = new();

        CommandCreator commandCreator = new(type);
        ConstantsCreator constantsCreator = new(type);
        DtoCreator dtoCreator = new(type);
        ModelCreator modelCreator = new(type);
        ProfileCreator profileCreator = new(type);
        QueryCreator queryCreator = new(type);
        RuleCreator ruleCreator = new(type);

        RepositoryCreator repositoryCreator = new(type);

        AltServiceCreator altServiceCreator = new(type);

        fileContents.Add(commandCreator.CreateEntitiyCommand());
        fileContents.Add(commandCreator.CreateEntitiyCommandHandler());
        fileContents.Add(commandCreator.CreateEntitiyCommandValidator());

        fileContents.Add(commandCreator.DeleteEntitiyCommand());
        fileContents.Add(commandCreator.DeleteEntitiyCommandHandler());
        fileContents.Add(commandCreator.DeleteEntitiyCommandValidator());


        fileContents.Add(commandCreator.UpdateEntitiyCommand());
        fileContents.Add(commandCreator.UpdateEntitiyCommandHandler());
        fileContents.Add(commandCreator.UpdateEntitiyCommandValidator());


        fileContents.Add(constantsCreator.ConstantsCreate());
        fileContents.Add(constantsCreator.EntityOperationClaimsCreate());


        fileContents.Add(dtoCreator.DtoCreate());
        fileContents.Add(dtoCreator.ListDtoCreate());
        fileContents.Add(dtoCreator.CreatedDtoCreate());
        fileContents.Add(dtoCreator.DeletedDtoCreate());
        fileContents.Add(dtoCreator.UpdatedDtoCreate());


        fileContents.Add(modelCreator.ListModelCreate());


        fileContents.Add(profileCreator.MappingProfileCreator());


        fileContents.Add(queryCreator.GetByIdEntityQuery());
        fileContents.Add(queryCreator.GetByIdEntityQueryHandler());

        fileContents.Add(queryCreator.GetListEntityQuery());
        fileContents.Add(queryCreator.GetListEntityQueryHandler());

        fileContents.Add(queryCreator.GetListEntityByDynamicQuery());
        fileContents.Add(queryCreator.GetListEntityByDynamicQueryHandler());


        fileContents.Add(ruleCreator.RuleCreate());


        string IRepository = IRepositorySelector(repositoryCreator, csFileOperationConfig.SelectedRepo);

        fileContents.Add(IRepository);
        fileContents.Add(repositoryCreator.Repository(csFileOperationConfig.GetDbContext));


        fileContents.Add(altServiceCreator.IServiceCreate());
        fileContents.Add(altServiceCreator.ServiceCreate());

        return fileContents;
    }

    private static string IRepositorySelector(RepositoryCreator repositoryCreator, byte selectedRepo)
    {
        return selectedRepo switch
        {
            1 => repositoryCreator.ISyncRepository(),
            2 => repositoryCreator.IAsyncRepository(),
            3 => repositoryCreator.ISyncAndAsyncRepository(),
            _ => repositoryCreator.IEmptyRepository(),
        };
    }
}
