using Core.Application.Dtos;

namespace Application.Features.Customers.Queries.GetList;

public class GetListCustomerListItemDto : IDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
}