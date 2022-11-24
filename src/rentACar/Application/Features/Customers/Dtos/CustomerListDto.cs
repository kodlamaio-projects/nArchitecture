using Core.Application.Dtos;

namespace Application.Features.Customers.Dtos;

public class CustomerListDto : IDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
}