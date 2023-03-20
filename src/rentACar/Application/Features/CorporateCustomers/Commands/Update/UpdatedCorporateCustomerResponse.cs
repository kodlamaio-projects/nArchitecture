using Core.Application.Responses;

namespace Application.Features.CorporateCustomers.Commands.Update;

public class UpdatedCorporateCustomerResponse : IResponse
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string TaxNo { get; set; }
}
