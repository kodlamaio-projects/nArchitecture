using Core.Application.Dtos;

namespace Application.Features.CorporateCustomers.Dtos;

public class CreatedCorporateCustomerDto : IDto
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string TaxNo { get; set; }
}