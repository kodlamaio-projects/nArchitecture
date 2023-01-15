namespace Generator.Codes.CreatorCodes;

internal class CsFileOperationConfig
{
    internal string GetDbContext;
    internal byte SelectedRepo;


    public void SetDbContextForType(Type type)
    {
        GetDbContext = type.Name;
    }
}
