using Core.Application.Responses;

namespace Application.Features.CorporateCustomers.Queries.GetByCustomerId;

public class GetByCustomerIdCorporateCustomerResponse : IResponse
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string TaxNo { get; set; }
}
