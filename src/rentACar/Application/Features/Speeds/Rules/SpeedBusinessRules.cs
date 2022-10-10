using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Speeds.Rules;

public class SpeedBusinessRules
{
    private readonly ISpeedRepository _speedRepository;

    public SpeedBusinessRules(ISpeedRepository speedRepository)
    {
        _speedRepository = speedRepository;
    }

    public async Task SpeedIdShouldExistWhenSelected(int id)
    {
        Speed? result = await _speedRepository.GetAsync(b => b.Id == id);
        if (result == null) throw new BusinessException("Speed not exists.");
    }

    public async Task SpeedNameCanNotBeDuplicatedWhenInserted(string name)
    {
        IPaginate<Speed> result = await _speedRepository.GetListAsync(b => b.Name == name);
        if (result.Items.Any()) throw new BusinessException("Speed name exists.");
    }
}