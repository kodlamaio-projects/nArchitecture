using Core.Application.Dtos;

namespace Application.Features.Invoices.Commands.Delete;

public class DeletedInvoiceResponse : IDto
{
    public int Id { get; set; }
}