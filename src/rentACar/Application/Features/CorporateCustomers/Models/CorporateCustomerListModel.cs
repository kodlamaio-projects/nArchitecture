using Application.Features.CorporateCustomers.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.CorporateCustomers.Models;

public class CorporateCustomerListModel : BasePageableModel
{
    public IList<CorporateCustomerListDto> Items { get; set; }
}
