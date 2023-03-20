using Core.Application.Responses;

namespace Application.Features.Customers.Queries.GetByUserId;

public class GetByUserIdCustomerResponse : IResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
}
