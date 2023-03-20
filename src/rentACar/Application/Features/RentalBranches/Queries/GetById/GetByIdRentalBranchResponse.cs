using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.RentalBranches.Queries.GetById;

public class GetByIdRentalBranchResponse : IResponse
{
    public int Id { get; set; }
    public City City { get; set; }
}
