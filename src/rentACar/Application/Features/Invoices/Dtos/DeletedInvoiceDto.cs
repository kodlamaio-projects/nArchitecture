using Core.Application.Dtos;

namespace Application.Features.Invoices.Dtos;

public class DeletedInvoiceDto : IDto
{
    public int Id { get; set; }
}