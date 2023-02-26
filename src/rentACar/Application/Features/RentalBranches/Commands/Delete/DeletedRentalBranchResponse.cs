using Core.Application.Dtos;

namespace Application.Features.RentalBranches.Commands.Delete;

public class DeletedRentalBranchResponse : IDto
{
    public int Id { get; set; }
}