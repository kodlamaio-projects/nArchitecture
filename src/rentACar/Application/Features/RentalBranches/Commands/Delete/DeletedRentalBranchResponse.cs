using Core.Application.Responses;

namespace Application.Features.RentalBranches.Commands.Delete;

public class DeletedRentalBranchResponse : IResponse
{
    public int Id { get; set; }
}
