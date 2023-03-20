using Core.Application.Responses;

namespace Application.Features.Fuels.Queries.GetById;

public class GetByIdFuelResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}
