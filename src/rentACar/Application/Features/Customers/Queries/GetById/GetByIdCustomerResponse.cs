using Core.Application.Dtos;

namespace Application.Features.Customers.Queries.GetById;

public class GetByIdCustomerResponse : IDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
}