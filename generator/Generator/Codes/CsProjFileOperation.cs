namespace Generator.Codes;

internal static class CsProjFileOperation
{
    public static (string Path, string FileName) CsProjOpenFileDialog()
    {
        OpenFileDialog fileDialog = new()
        {
            InitialDirectory = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.Personal)).FullName,
            Multiselect = false,
            Filter = "C# Project File |*.csproj"
        };

        MessageBox.Show("Please select the layer \".csproj\" file within your project.", "Warning");

        if (fileDialog.ShowDialog() != DialogResult.OK)
            MessageBox.Show(new Exception("No file selected").Message);

        string path = Path.GetDirectoryName(fileDialog.FileName);
        path = Path.GetDirectoryName(path);

        return (path, fileDialog.SafeFileName);
    }
}
