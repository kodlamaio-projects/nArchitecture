using Core.Application.Dtos;

namespace Application.Features.CorporateCustomers.Queries.GetList;

public class GetListCorporateCustomerListItemDto : IDto
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string TaxNo { get; set; }
}
