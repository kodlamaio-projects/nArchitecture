using Core.Persistence.Repositories;

namespace Domain.Entities;

public class RentalsAdditionalService : Entity
{
    public int RentalId { get; set; }
    public int AdditionalServiceId { get; set; }

    public virtual Rental Rental { get; set; }
    public virtual AdditionalService AdditionalService { get; set; }

    public RentalsAdditionalService() { }

    public RentalsAdditionalService(int id, int rentalId, int additionalServiceId)
        : base(id)
    {
        RentalId = rentalId;
        AdditionalServiceId = additionalServiceId;
    }
}
