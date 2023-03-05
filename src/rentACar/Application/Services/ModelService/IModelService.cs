using Domain.Entities;

namespace Application.Services.ModelService;

public interface IModelService
{
    public Task<Model> GetById(int id);
}
