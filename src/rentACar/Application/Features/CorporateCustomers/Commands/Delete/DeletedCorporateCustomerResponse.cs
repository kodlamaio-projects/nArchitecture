using Core.Application.Responses;

namespace Application.Features.CorporateCustomers.Commands.Delete;

public class DeletedCorporateCustomerResponse : IResponse
{
    public int Id { get; set; }
}
