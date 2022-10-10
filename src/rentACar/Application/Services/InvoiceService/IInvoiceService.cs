using Domain.Entities;

namespace Application.Services.InvoiceService;

public interface IInvoiceService
{
    public Task<Invoice> CreateInvoice(Rental rental, decimal dailyPrice);
    public Task<Invoice> Add(Invoice invoice);
}