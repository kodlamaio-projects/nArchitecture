using Core.Application.Responses;

namespace Application.Features.Invoices.Queries.GetById;

public class GetByIdInvoiceResponse : IResponse
{
    public int CustomerId { get; set; }
    public string No { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime RentalStartDate { get; set; }
    public DateTime RentalEndDate { get; set; }
    public short TotalRentalDate { get; set; }
    public decimal RentalPrice { get; set; }
}
