using Core.Domain.Abstract;
using Core.Domain.Bases;

namespace Domain.Entities;

public class Color : BaseEntity<int>, IEntity
{
    public virtual ICollection<Car> Cars { get; set; }

    public Color()
    {
        Cars = new HashSet<Car>();
    }

    public Color(int id, string name) : this()
    {
        Id = id;
        Name = name;
    }
}