using Core.Application.Dtos;

namespace Application.Features.Rentals.Commands.Delete;

public class DeletedRentalResponse : IDto
{
    public int Id { get; set; }
}