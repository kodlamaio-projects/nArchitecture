using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;

namespace Application.Services.FindeksCreditRateService;

public class FindeksCreditRateManager : IFindeksCreditRateService
{
    private readonly IFindeksCreditRateRepository _findeksCreditRateRepository;

    public FindeksCreditRateManager(IFindeksCreditRateRepository findeksCreditRateRepository)
    {
        _findeksCreditRateRepository = findeksCreditRateRepository;
    }

    public async Task<FindeksCreditRate> GetFindeksCreditRateByCustomerId(int customerId)
    {
        FindeksCreditRate findeksCreditRate =
            await _findeksCreditRateRepository.GetAsync(f => f.CustomerId == customerId);
        if (findeksCreditRate == null) throw new BusinessException("Customer's findeks score do not exists.");
        return findeksCreditRate;
    }

    public async Task<FindeksCreditRate> Add(FindeksCreditRate findeksCreditRate)
    {
        FindeksCreditRate addedFindeksCreditRate = await _findeksCreditRateRepository.AddAsync(findeksCreditRate);
        return addedFindeksCreditRate;
    }
}