namespace Core.Domain.Bases;

public class BaseEntity<I> where I : struct
{
    public BaseEntity() { }

    public BaseEntity(I id) : this()
    {
        Id = id;
    }

    public I Id { get; set; }

    public string Name { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public bool IsDeleted { get; set; } = false;
}
