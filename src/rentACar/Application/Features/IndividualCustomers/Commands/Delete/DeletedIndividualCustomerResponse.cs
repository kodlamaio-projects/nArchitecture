using Core.Application.Responses;

namespace Application.Features.IndividualCustomers.Commands.Delete;

public class DeletedIndividualCustomerResponse : IResponse
{
    public int Id { get; set; }
}
