using Core.Application.Responses;

namespace Application.Features.Invoices.Commands.Delete;

public class DeletedInvoiceResponse : IResponse
{
    public int Id { get; set; }
}
