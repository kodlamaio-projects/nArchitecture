using Core.Application.Dtos;

namespace Application.Features.IndividualCustomers.Commands.Delete;

public class DeletedIndividualCustomerResponse : IDto
{
    public int Id { get; set; }
}
