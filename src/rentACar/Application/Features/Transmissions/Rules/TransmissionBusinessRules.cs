using Application.Features.Transmissions.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Transmissions.Rules;

public class TransmissionBusinessRules : BaseBusinessRules
{
    private readonly ITransmissionRepository _transmissionRepository;

    public TransmissionBusinessRules(ITransmissionRepository transmissionRepository)
    {
        _transmissionRepository = transmissionRepository;
    }

    public async Task TransmissionIdShouldExistWhenSelected(int id)
    {
        Transmission? result = await _transmissionRepository.GetAsync(predicate: b => b.Id == id, enableTracking: false);
        if (result == null)
            throw new BusinessException(TransmissionsMessages.TransmissionNotExists);
    }

    public async Task TransmissionNameCanNotBeDuplicatedWhenInserted(string name)
    {
        IPaginate<Transmission> result = await _transmissionRepository.GetListAsync(predicate: b => b.Name == name, enableTracking: false);
        if (result.Items.Any())
            throw new BusinessException(TransmissionsMessages.TransmissionNameExists);
    }
}
