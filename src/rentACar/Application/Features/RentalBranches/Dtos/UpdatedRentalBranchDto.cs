using Domain.Enums;

namespace Application.Features.RentalBranches.Dtos;

public class UpdatedRentalBranchDto
{
    public int Id { get; set; }
    public City City { get; set; }
}