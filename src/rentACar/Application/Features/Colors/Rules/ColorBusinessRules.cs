using Application.Features.Colors.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Colors.Rules;

public class ColorBusinessRules : BaseBusinessRules
{
    private readonly IColorRepository _colorRepository;

    public ColorBusinessRules(IColorRepository colorRepository)
    {
        _colorRepository = colorRepository;
    }

    public async Task ColorIdShouldExistWhenSelected(int id)
    {
        Color? result = await _colorRepository.GetAsync(b => b.Id == id, enableTracking: false);
        if (result == null) throw new BusinessException(ColorsMessages.ColorNotExists);
    }

    public async Task ColorNameCanNotBeDuplicatedWhenInserted(string name)
    {
        IPaginate<Color> result = await _colorRepository.GetListAsync(b => b.Name == name, enableTracking: false);
        if (result.Items.Any()) throw new BusinessException(ColorsMessages.ColorNameExists);
    }
}