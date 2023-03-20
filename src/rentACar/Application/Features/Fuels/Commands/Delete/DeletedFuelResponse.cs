using Core.Application.Responses;

namespace Application.Features.Fuels.Commands.Delete;

public class DeletedFuelResponse : IResponse
{
    public int Id { get; set; }
}
