using Generator.Codes.CreatorCodes;

namespace Generator.Codes;

internal static class CodeGeneratorHelpers
{
    public static string[] FindZeroAndReplaceEntity(string[] stringList, Type type)
    {
        List<string> names = new();

        foreach (string str in stringList)
        {
            string NewName = FindZeroAndReplaceEntity(str, type);
            names.Add(NewName);
        }

        return names.ToArray();
    }

    public static string FindZeroAndReplaceEntity(string name, Type type)
    {
        if (name.Any(s => s == '0'))
            return $@"{name.Replace("0", type.Name)}";

        return $@"{name + type.Name}";
    }

    public static void WriteCsFiles(IList<CsFile> csFiles, string sPath)
    {
        foreach (CsFile csFile in csFiles)
        {
            WriteCsFile(csFile, sPath);
        }
    }

    public static void WriteCsFile(CsFile csFile, string sPath)
    {
        string path = $"{sPath}\\{csFile.Path}";

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        string fullPath = $"{path}\\{csFile.FileName}.cs";

        if (!File.Exists(fullPath))
        {
            using FileStream fs = File.Create(fullPath);

            using TextWriter tw = new StreamWriter(fs);

            tw.Write(csFile.FileContent);

            tw.Flush();
        }
    }
}