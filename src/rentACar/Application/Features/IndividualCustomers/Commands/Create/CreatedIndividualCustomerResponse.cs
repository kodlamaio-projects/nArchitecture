using Core.Application.Responses;

namespace Application.Features.IndividualCustomers.Commands.Create;

public class CreatedIndividualCustomerResponse : IResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalIdentity { get; set; }
}
