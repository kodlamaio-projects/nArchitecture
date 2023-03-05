using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Invoice : Entity
{
    public int CustomerId { get; set; }
    public string No { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime RentalStartDate { get; set; }
    public DateTime RentalEndDate { get; set; }
    public short TotalRentalDate { get; set; }
    public decimal RentalPrice { get; set; }

    public virtual Customer? Customer { get; set; }

    public Invoice() { }

    public Invoice(
        int id,
        int customerId,
        string no,
        DateTime createdDate,
        DateTime rentalStartDate,
        DateTime rentalEndDate,
        short totalRentalDate,
        decimal rentalPrice
    )
        : base(id)
    {
        CustomerId = customerId;
        No = no;
        CreatedDate = createdDate;
        RentalStartDate = rentalStartDate;
        RentalEndDate = rentalEndDate;
        TotalRentalDate = totalRentalDate;
        RentalPrice = rentalPrice;
    }
}
