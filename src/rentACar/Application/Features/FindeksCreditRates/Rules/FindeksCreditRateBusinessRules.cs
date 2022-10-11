using Application.Features.FindeksCreditRates.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;

namespace Application.Features.FindeksCreditRates.Rules;

public class FindeksCreditRateBusinessRules
{
    private readonly IFindeksCreditRateRepository _findeksCreditRateRepository;

    public FindeksCreditRateBusinessRules(IFindeksCreditRateRepository findeksCreditRateRepository)
    {
        _findeksCreditRateRepository = findeksCreditRateRepository;
    }

    public async Task FindeksCreditRateIdShouldExistWhenSelected(int id)
    {
        FindeksCreditRate? result = await _findeksCreditRateRepository.GetAsync(b => b.Id == id);
        if (result == null) throw new BusinessException(FindeksCreditRateMessage.FindeksCreditRateNotExists);
    }

    public Task FindeksCreditShouldBeExist(FindeksCreditRate? findeksCreditRate)
    {
        if (findeksCreditRate is null) throw new BusinessException(FindeksCreditRateMessage.FindeksCreditRateNotExists);
        return Task.CompletedTask;
    }
}