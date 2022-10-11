﻿using Application.Features.Colors.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Colors.Rules;

public class ColorBusinessRules
{
    private readonly IColorRepository _colorRepository;

    public ColorBusinessRules(IColorRepository colorRepository)
    {
        _colorRepository = colorRepository;
    }

    public async Task ColorIdShouldExistWhenSelected(int id)
    {
        Color? result = await _colorRepository.GetAsync(b => b.Id == id);
        if (result == null) throw new BusinessException(ColorExceptionMessages.ColorNotExistsMessage);
    }

    public async Task ColorNameCanNotBeDuplicatedWhenInserted(string name)
    {
        IPaginate<Color> result = await _colorRepository.GetListAsync(b => b.Name == name);
        if (result.Items.Any()) throw new BusinessException(ColorExceptionMessages.ColorNameExistsMessage);
    }
}