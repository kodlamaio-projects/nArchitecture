using Core.Persistence.Repositories;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Rental : Entity
{
    public int CarId { get; set; }
    public int CustomerId { get; set; }
    //public int RentStartRentalBranchId { get; set; }
    //public int? RentEndRentalBranchId { get; set; }
    public DateTime RentStartDate { get; set; }
    public DateTime RentEndDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public int RentStartKilometer { get; set; }
    public int? RentEndKilometer { get; set; }

    public virtual Car? Car { get; set; }
    public virtual Customer? Customer { get; set; }
    //public virtual RentalBranch? RentStartRentalBranch { get; set; }
    //public virtual RentalBranch? RentEndRentalBranch { get; set; }
    public virtual ICollection<RentalsAdditionalService> RentalsAdditionalServices { get; set; }

    public Rental()
    {
        RentalsAdditionalServices = new HashSet<RentalsAdditionalService>();
    }

    public Rental(int id, int customerId, int carId, int rentStartRentalBranchId, int rentEndRentalBranchId,
                  DateTime rentStartDate, DateTime rentEndDate, DateTime? returnDate, int rentStartKilometer,
                  int rentEndKilometer) : this()
    {
        Id = id;
        CustomerId = customerId;
        CarId = carId;
        //RentStartRentalBranchId = rentStartRentalBranchId;
        //RentEndRentalBranchId = rentEndRentalBranchId;
        RentStartDate = rentStartDate;
        RentEndDate = rentEndDate;
        ReturnDate = returnDate;
        RentStartKilometer = rentStartKilometer;
        RentEndKilometer = rentEndKilometer;
    }
}