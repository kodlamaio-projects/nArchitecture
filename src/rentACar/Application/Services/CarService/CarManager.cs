using Application.Services.RentalService;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.CarService;

public class CarManager : ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IRentalService _rentalService;

    public CarManager(ICarRepository carRepository, IRentalService rentalService)
    {
        _carRepository = carRepository;
        _rentalService = rentalService;
    }

    public async Task<Car> GetById(int id)
    {
        Car car = await _carRepository.GetAsync(c => c.Id == id);
        if (car == null) throw new NotFoundException("The car doesn't exist.");
        return car;
    }


    public async Task<Car> PickUpCar(Rental rental)
    {
        Car carToBeUpdate = await _carRepository.GetAsync(c => c.Id == rental.CarId);
        carToBeUpdate.Kilometer += Convert.ToInt32(rental.RentEndKilometer - rental.RentStartKilometer);
        carToBeUpdate.CarState = CarState.Available;
        Car updatedCar = await _carRepository.UpdateAsync(carToBeUpdate);
        return updatedCar;
    }

    public async Task<Car?> GetAvailableCarToRent(int modelId, int rentStartRentalBranch, DateTime rentStartDate,
                                                  DateTime rentEndDate)
    {
        Car? carToFind = await _carRepository.GetAsync(c => c.ModelId == modelId &&
                                                            c.RentalBranchId == rentStartRentalBranch &&
                                                            !c.Rentals.Any(r => r.RentStartDate <= rentStartDate && r.RentEndDate >= rentEndDate),
                                                            include: i => i.Include(i => i.Rentals));
        if (carToFind != null) return carToFind;
        throw new NotFoundException("Available car doesn't exist.");
    }
}