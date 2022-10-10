using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Speed : Entity
{
    public string Name { get; set; }

    public virtual ICollection<Car> Cars { get; set; }

    public Speed()
    {
        Cars = new HashSet<Car>();
    }

    public Speed(int id, string name) : this()
    {
        Id = id;
        Name = name;
    }
}