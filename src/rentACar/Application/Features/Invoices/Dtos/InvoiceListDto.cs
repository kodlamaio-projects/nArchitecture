namespace Application.Features.Invoices.Dtos;

public class InvoiceListDto
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public string No { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime RentalStartDate { get; set; }
    public DateTime RentalEndDate { get; set; }
    public short TotalRentalDate { get; set; }
    public decimal RentalPrice { get; set; }
}