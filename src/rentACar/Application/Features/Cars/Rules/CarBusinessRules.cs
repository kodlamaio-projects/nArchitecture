using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using Domain.Enums;

namespace Application.Features.Cars.Rules;

public class CarBusinessRules
{
    private readonly ICarRepository _carRepository;

    public CarBusinessRules(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task CarIdShouldExistWhenSelected(int id)
    {
        Car? result = await _carRepository.GetAsync(c => c.Id == id);
        if (result == null) throw new BusinessException("Car not exists.");
    }

    public async Task CarCanNotBeMaintainWhenIsRented(int id)
    {
        Car? car = await _carRepository.GetAsync(c => c.Id == id);
        if (car.CarState == CarState.Rented) throw new BusinessException("Car can't be maintain when is rented.");
    }

    public async Task CarCanNotBeRentWhenIsInMaintenance(int carId)
    {
        Car? car = await _carRepository.GetAsync(c => c.Id == carId);
        if (car.CarState == CarState.Maintenance)
            throw new BusinessException("Car can not be rent when is in maintenance.");
    }

    public async Task CarCanNotBeRentWhenIsRented(int carId)
    {
        Car? car = await _carRepository.GetAsync(c => c.Id == carId);
        if (car.CarState == CarState.Rented)
            throw new BusinessException("Car can not be rent when is rented.");
    }
}