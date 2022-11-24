using Core.Application.Dtos;

namespace Application.Features.Customers.Dtos;

public class CustomerDto : IDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
}