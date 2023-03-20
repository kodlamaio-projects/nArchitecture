using Core.Application.Responses;

namespace Application.Features.Customers.Commands.Delete;

public class DeletedCustomerResponse : IResponse
{
    public int Id { get; set; }
}
