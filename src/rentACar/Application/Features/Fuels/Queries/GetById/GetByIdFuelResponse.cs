using Core.Application.Dtos;

namespace Application.Features.Fuels.Queries.GetById;

public class GetByIdFuelResponse : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}