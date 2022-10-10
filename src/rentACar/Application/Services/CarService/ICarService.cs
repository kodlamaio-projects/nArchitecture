using Domain.Entities;

namespace Application.Services.CarService;

public interface ICarService
{
    public Task<Car> GetById(int Id);
    public Task<Car> PickUpCar(Rental rental);

    public Task<Car?> GetAvailableCarToRent(int modelId, int rentStartRentalBranch,
                                            DateTime rentStartDate, DateTime rentEndDate);
}