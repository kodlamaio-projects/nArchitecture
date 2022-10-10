using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;

namespace Application.Services.ModelService;

public class ModelManager : IModelService
{
    private readonly IModelRepository _modelRepository;

    public ModelManager(IModelRepository modelRepository)
    {
        _modelRepository = modelRepository;
    }

    public async Task<Model> GetById(int id)
    {
        Model model = await _modelRepository.GetAsync(m => m.Id == id);
        if (model == null) throw new BusinessException("Model doesn't exist.");
        return model;
    }
}