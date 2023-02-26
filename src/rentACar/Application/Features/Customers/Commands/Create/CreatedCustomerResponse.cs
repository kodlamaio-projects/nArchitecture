using Core.Application.Dtos;

namespace Application.Features.Customers.Commands.Create;

public class CreatedCustomerResponse : IDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
}