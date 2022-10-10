using Application.Features.RentalBranches.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.RentalBranches.Models;

public class RentalBranchListModel : BasePageableModel
{
    public IList<RentalBranchListDto> Items { get; set; }
}