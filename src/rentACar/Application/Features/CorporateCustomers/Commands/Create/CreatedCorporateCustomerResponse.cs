using Core.Application.Dtos;

namespace Application.Features.CorporateCustomers.Commands.Create;

public class CreatedCorporateCustomerResponse : IDto
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string TaxNo { get; set; }
}
