using Core.Persistence.Repositories;

namespace Core.Test.Application.FakeData;

public abstract class BaseFakeData<TEntity>
    where TEntity : Entity, new()
{
    public List<TEntity> Data => CreateFakeData();
    public abstract List<TEntity> CreateFakeData();
}
