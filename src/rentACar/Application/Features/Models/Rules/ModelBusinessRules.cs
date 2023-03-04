using Application.Features.Models.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Models.Rules;

public class ModelBusinessRules : BaseBusinessRules
{
    private readonly IModelRepository _modelRepository;

    public ModelBusinessRules(IModelRepository modelRepository)
    {
        _modelRepository = modelRepository;
    }

    public async Task ModelIdShouldExistWhenSelected(int id)
    {
        Model? result = await _modelRepository.GetAsync(c => c.Id == id, enableTracking: false);
        if (result == null) throw new BusinessException(ModelsMessages.ModelNotExists);
    }
}