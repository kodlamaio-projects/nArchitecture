using Core.Application.Responses;

namespace Application.Features.Rentals.Commands.Delete;

public class DeletedRentalResponse : IResponse
{
    public int Id { get; set; }
}
