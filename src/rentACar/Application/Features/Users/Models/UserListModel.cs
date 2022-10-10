using Application.Features.Users.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.Users.Models;

public class UserListModel : BasePageableModel
{
    public IList<UserListDto> Items { get; set; }
}