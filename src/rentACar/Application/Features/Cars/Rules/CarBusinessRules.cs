using Application.Features.Cars.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using Domain.Enums;

namespace Application.Features.Cars.Rules;

public class CarBusinessRules : BaseBusinessRules
{
    private readonly ICarRepository _carRepository;

    public CarBusinessRules(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task CarIdShouldExistWhenSelected(int id)
    {
        Car? result = await _carRepository.GetAsync(c => c.Id == id, enableTracking: false);
        if (result == null) throw new BusinessException(CarMessages.CarNotExists);
    }

    public async Task CarCanNotBeMaintainWhenIsRented(int id)
    {
        Car? car = await _carRepository.GetAsync(c => c.Id == id, enableTracking: false);
        if (car.CarState == CarState.Rented) throw new BusinessException(CarMessages.CarCanNotBeMaintainWhenIsRented);
    }

    public async Task CarCanNotBeRentWhenIsInMaintenance(int carId)
    {
        Car? car = await _carRepository.GetAsync(c => c.Id == carId, enableTracking: false);
        if (car.CarState == CarState.Maintenance)
            throw new BusinessException(CarMessages.CarCanNotBeRentWhenIsInMaintenance);
    }

    public async Task CarCanNotBeRentWhenIsRented(int carId)
    {
        Car? car = await _carRepository.GetAsync(c => c.Id == carId, enableTracking: false);
        if (car.CarState == CarState.Rented)
            throw new BusinessException(CarMessages.CarCanNotBeRentWhenIsRented);
    }
}