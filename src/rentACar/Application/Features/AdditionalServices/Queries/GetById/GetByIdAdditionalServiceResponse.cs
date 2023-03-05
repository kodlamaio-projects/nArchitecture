using Core.Application.Dtos;

namespace Application.Features.AdditionalServices.Queries.GetById;

public class GetByIdAdditionalServiceResponse : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
}
