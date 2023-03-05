namespace Core.Persistence.Dynamic;

public class Sort
{
    public string Field { get; set; }
    public string Dir { get; set; }

    public Sort() { }

    public Sort(string field, string dir)
    {
        Field = field;
        Dir = dir;
    }
}
