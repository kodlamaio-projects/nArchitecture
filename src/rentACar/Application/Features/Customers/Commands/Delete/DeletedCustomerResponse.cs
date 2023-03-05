using Core.Application.Dtos;

namespace Application.Features.Customers.Commands.Delete;

public class DeletedCustomerResponse : IDto
{
    public int Id { get; set; }
}
