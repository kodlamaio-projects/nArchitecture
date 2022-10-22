using Core.Domain.Abstract;
using Core.Domain.Bases;

namespace Domain.Entities;

public class Brand : BaseEntity<int>, IEntity
{
    public virtual ICollection<Model> Models { get; set; }

    public Brand()
    {
        Models = new HashSet<Model>();
    }

    public Brand(int id, string name) : this()
    {
        Id = id;
        Name = name;
    }
}