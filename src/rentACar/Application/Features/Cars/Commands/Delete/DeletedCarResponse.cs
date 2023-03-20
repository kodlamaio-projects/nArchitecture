using Core.Application.Responses;

namespace Application.Features.Cars.Commands.Delete;

public class DeletedCarResponse : IResponse
{
    public int Id { get; set; }
}
