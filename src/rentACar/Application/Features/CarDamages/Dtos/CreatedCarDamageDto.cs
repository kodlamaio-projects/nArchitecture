namespace Application.Features.CarDamages.Dtos;

public class CreatedCarDamageDto
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public string DamageDescription { get; set; }
    public bool IsFixed { get; set; }
}