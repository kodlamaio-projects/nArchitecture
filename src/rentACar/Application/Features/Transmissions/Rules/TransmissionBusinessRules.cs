using Application.Features.Transmissions.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Transmissions.Rules;

public class TransmissionBusinessRules
{
    private readonly ITransmissionRepository _transmissionRepository;

    public TransmissionBusinessRules(ITransmissionRepository transmissionRepository)
    {
        _transmissionRepository = transmissionRepository;
    }

    public async Task TransmissionIdShouldExistWhenSelected(int id)
    {
        Transmission? result = await _transmissionRepository.GetAsync(b => b.Id == id);
        if (result == null) throw new BusinessException("Transmission not exists.");
    }

    public async Task TransmissionNameCanNotBeDuplicatedWhenInserted(string name)
    {
        IPaginate<Transmission> result = await _transmissionRepository.GetListAsync(b => b.Name == name);
        if (result.Items.Any()) throw new BusinessException(TransmissionExceptionMessages.TransmissionNotExistsMessage);
    }
}