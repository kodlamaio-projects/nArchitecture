using Core.Application.Responses;

namespace Application.Features.Brands.Commands.Create;

public class CreatedBrandResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}
