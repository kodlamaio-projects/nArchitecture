using Core.Application.Responses;

namespace Application.Features.Fuels.Commands.Create;

public class CreatedFuelResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}
