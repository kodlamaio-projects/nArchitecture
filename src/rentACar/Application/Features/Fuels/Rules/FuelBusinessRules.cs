﻿using Application.Features.Fuels.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Fuels.Rules;

public class FuelBusinessRules : BaseBusinessRules
{
    private readonly IFuelRepository _fuelRepository;

    public FuelBusinessRules(IFuelRepository fuelRepository)
    {
        _fuelRepository = fuelRepository;
    }

    public async Task FuelIdShouldExistWhenSelected(int id)
    {
        Fuel? result = await _fuelRepository.GetAsync(b => b.Id == id);
        if (result == null) throw new BusinessException(FuelMessages.FuelNotExists);
    }

    public async Task FuelNameCanNotBeDuplicatedWhenInserted(string name)
    {
        IPaginate<Fuel> result = await _fuelRepository.GetListAsync(b => b.Name == name);
        if (result.Items.Any()) throw new BusinessException(FuelMessages.FuelNameExists);
    }
}