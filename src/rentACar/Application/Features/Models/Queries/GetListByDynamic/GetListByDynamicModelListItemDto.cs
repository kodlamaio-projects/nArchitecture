using Core.Application.Dtos;

namespace Application.Features.Models.Queries.GetListByDynamic;

public class GetListByDynamicModelListItemDto : IDto
{
    public int Id { get; set; }
    public string BrandName { get; set; }
    public string FuelName { get; set; }
    public string TransmissionName { get; set; }
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
    public string ImageUrl { get; set; }
}