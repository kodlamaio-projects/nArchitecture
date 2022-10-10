using Application.Features.UserOperationClaims.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.UserOperationClaims.Models;

public class UserOperationClaimListModel : BasePageableModel
{
    public IList<UserOperationClaimListDto> Items { get; set; }
}