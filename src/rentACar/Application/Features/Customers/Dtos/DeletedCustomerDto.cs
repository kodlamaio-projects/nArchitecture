using Core.Application.Dtos;

namespace Application.Features.Customers.Dtos;

public class DeletedCustomerDto : IDto
{
    public int Id { get; set; }
}