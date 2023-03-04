using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.RentalBranches.Commands.Update;

public class UpdatedRentalBranchResponse : IDto
{
    public int Id { get; set; }
    public City City { get; set; }
}