using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.RentalBranches.Dtos;

public class RentalBranchDto : IDto
{
    public int Id { get; set; }
    public City City { get; set; }
}