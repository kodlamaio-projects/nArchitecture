using Core.Domain.Abstract;
using Core.Domain.Bases;

namespace Domain.Entities;

public class CarDamage : BaseEntity<int>, IEntity
{
    public int CarId { get; set; }
    public string DamageDescription { get; set; }
    public bool IsFixed { get; set; }

    public virtual Car? Car { get; set; }

    public CarDamage() { }

    public CarDamage(int id, int carId, string damageDescription, bool isFixed) : base(id)
    {
        CarId = carId;
        DamageDescription = damageDescription;
        IsFixed = isFixed;
    }
}