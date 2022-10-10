using Application.Features.IndividualCustomers.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.IndividualCustomers.Models;

public class IndividualCustomerListModel : BasePageableModel
{
    public IList<IndividualCustomerListDto> Items { get; set; }
}
