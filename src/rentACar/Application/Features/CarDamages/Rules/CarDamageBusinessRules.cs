using Application.Features.CarDamages.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;

namespace Application.Features.CarDamages.Rules;

public class CarDamageBusinessRules : BaseBusinessRules
{
    private readonly ICarDamageRepository _carDamageRepository;

    public CarDamageBusinessRules(ICarDamageRepository carDamageRepository)
    {
        _carDamageRepository = carDamageRepository;
    }

    public async Task CarDamageIdShouldExistWhenSelected(int id)
    {
        CarDamage? result = await _carDamageRepository.GetAsync(b => b.Id == id, enableTracking: false);
        if (result == null) throw new BusinessException(CarDamagesMessages.CarDamageNotExists);
    }
}