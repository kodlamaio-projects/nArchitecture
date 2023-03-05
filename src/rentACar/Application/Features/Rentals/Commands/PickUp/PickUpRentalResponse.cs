using Core.Application.Dtos;

namespace Application.Features.Rentals.Commands.PickUp;

public class PickUpRentalResponse : IDto
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public int CustomerId { get; set; }
    public int RentStartRentalBranchId { get; set; }
    public int? RentEndRentalBranchId { get; set; }
    public DateTime RentStartDate { get; set; }
    public DateTime RentEndDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public int RentStartKilometer { get; set; }
    public int? RentEndKilometer { get; set; }
}
