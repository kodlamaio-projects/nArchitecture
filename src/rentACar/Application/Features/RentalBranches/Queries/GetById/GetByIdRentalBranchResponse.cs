using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.RentalBranches.Queries.GetById;

public class GetByIdRentalBranchResponse : IDto
{
    public int Id { get; set; }
    public City City { get; set; }
}