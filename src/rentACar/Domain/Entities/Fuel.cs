using Core.Domain.Abstract;
using Core.Domain.Bases;

namespace Domain.Entities;

public class Fuel : BaseEntity<int>, IEntity
{
    public virtual ICollection<Model> Models { get; set; }

    public Fuel()
    {
        Models = new HashSet<Model>();
    }

    public Fuel(int id, string name) : this()

    {
        Id = id;
        Name = name;
    }
}