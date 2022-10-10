using Domain.Entities;

namespace Application.Services.RentalService;

public interface IRentalService
{
    Task<IList<Rental>> GetAllByInDates(int carId, DateTime rentStartDate, DateTime rentEndDate);
}