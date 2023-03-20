using Core.Application.Responses;

namespace Application.Features.IndividualCustomers.Queries.GetByCustomerId;

public class GetByCustomerIdIndividualCustomerResponse : IResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalIdentity { get; set; }
}
