using Core.Application.Dtos;

namespace Application.Features.IndividualCustomers.Commands.Update;

public class UpdatedIndividualCustomerResponse : IDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalIdentity { get; set; }
}