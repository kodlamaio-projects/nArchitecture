using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.RentalBranches.Queries.GetList;

public class GetListRentalBranchListItemDto : IDto
{
    public int Id { get; set; }
    public City City { get; set; }
}