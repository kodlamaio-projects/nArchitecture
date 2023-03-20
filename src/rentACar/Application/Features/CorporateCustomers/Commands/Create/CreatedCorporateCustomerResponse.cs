using Core.Application.Responses;

namespace Application.Features.CorporateCustomers.Commands.Create;

public class CreatedCorporateCustomerResponse : IResponse
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string TaxNo { get; set; }
}
