using Core.Application.Responses;

namespace Application.Features.Models.Commands.Delete;

public class DeletedModelResponse : IResponse
{
    public int Id { get; set; }
}
