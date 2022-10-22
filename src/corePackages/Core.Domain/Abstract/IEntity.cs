namespace Core.Domain.Abstract;

public interface IEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}