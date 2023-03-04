using Core.Application.Dtos;

namespace Application.Features.Colors.Queries.GetById;

public class GetByIdColorResponse : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}