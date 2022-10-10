using Domain.Enums;

namespace Application.Features.RentalBranches.Dtos;

public class RentalBranchListDto
{
    public int Id { get; set; }
    public City City { get; set; }
}