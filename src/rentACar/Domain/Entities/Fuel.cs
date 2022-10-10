using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Fuel : Entity
{
    public string Name { get; set; }

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