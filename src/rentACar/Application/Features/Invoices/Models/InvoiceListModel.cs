using Application.Features.Invoices.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.Invoices.Models;

public class InvoiceListModel : BasePageableModel
{
    public IList<InvoiceListDto> Items { get; set; }
}
