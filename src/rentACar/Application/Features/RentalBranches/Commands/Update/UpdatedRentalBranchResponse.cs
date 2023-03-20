using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.RentalBranches.Commands.Update;

public class UpdatedRentalBranchResponse : IResponse
{
    public int Id { get; set; }
    public City City { get; set; }
}
