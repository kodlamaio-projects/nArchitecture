using Domain.Enums;

namespace Application.Features.RentalBranches.Dtos;

public class CreatedRentalBranchDto
{
    public int Id { get; set; }
    public City City { get; set; }
}