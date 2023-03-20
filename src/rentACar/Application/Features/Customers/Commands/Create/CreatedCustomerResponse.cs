using Core.Application.Responses;

namespace Application.Features.Customers.Commands.Create;

public class CreatedCustomerResponse : IResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
}
