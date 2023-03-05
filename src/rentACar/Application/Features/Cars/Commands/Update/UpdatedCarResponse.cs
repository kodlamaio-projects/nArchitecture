using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.Cars.Commands.Update;

public class UpdatedCarResponse : IDto
{
    public int Id { get; set; }
    public int ColorId { get; set; }
    public int ModelId { get; set; }
    public int RentalBranchId { get; set; }
    public CarState CarState { get; set; }
    public int Kilometer { get; set; }
    public short ModelYear { get; set; }
    public string Plate { get; set; }
    public short MinFindeksCreditRate { get; set; }
}
