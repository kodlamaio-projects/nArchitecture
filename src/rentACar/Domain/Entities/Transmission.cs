using Core.Domain.Abstract;
using Core.Domain.Bases;

namespace Domain.Entities;

public class Transmission : BaseEntity<int>, IEntity
{
    public virtual ICollection<Model> Models { get; set; }

    public Transmission()
    {
        Models = new HashSet<Model>();
    }

    public Transmission(int id, string name) : this()
    {
        Id = id;
        Name = name;
    }
}