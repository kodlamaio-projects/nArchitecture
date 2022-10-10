using Core.Persistence.Repositories;
using Domain.Enums;

namespace Domain.Entities;

public class Car : Entity
{
    public int ColorId { get; set; }
    public int ModelId { get; set; }
    public int RentalBranchId { get; set; }
    public CarState CarState { get; set; }
    public int Kilometer { get; set; }
    public short ModelYear { get; set; }
    public string Plate { get; set; }
    public short MinFindeksCreditRate { get; set; }

    public virtual Color? Color { get; set; }
    public virtual RentalBranch? RentalBranch { get; set; }
    public virtual Model? Model { get; set; }
    public virtual ICollection<CarDamage> CarDamages { get; set; }

    public Car()
    {
        CarDamages = new HashSet<CarDamage>();
    }

    public Car(int id, int colorId, int modelId, int rentalBranchId, CarState carState, int kilometer,
               short modelYear,
               string plate,
               short minFindeksCreditRate) : this()
    {
        Id = id;
        ColorId = colorId;
        ModelId = modelId;
        RentalBranchId = rentalBranchId;
        CarState = carState;
        Kilometer = kilometer;
        ModelYear = modelYear;
        Plate = plate;
        MinFindeksCreditRate = minFindeksCreditRate;
    }
}