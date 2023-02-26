using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.Cars.Queries.GetList;

public class GetListCarListItemDto : IDto
{
    public int Id { get; set; }
    public string BrandName { get; set; }
    public string ModelName { get; set; }
    public string ColorName { get; set; }
    public string Plate { get; set; }
    public CarState CarState { get; set; }
    public short ModelYear { get; set; }
}