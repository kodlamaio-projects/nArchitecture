using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.RentalBranches.Dtos;

public class UpdatedRentalBranchDto : IDto
{
    public int Id { get; set; }
    public City City { get; set; }
}