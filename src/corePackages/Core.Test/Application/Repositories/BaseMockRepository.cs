using AutoMapper;
using Core.Application.Rules;
using Core.Persistence.Repositories;
using Core.Test.Application.FakeData;
using Core.Test.Application.Helpers;
using Moq;

namespace Core.Test.Application.Repositories;

public abstract class BaseMockRepository<TRepository, TEntity, TMappingProfile, TBusinessRules, TFakeData>
    where TEntity : Entity, new()
    where TRepository : class, IAsyncRepository<TEntity>, IRepository<TEntity>
    where TMappingProfile : Profile, new()
    where TBusinessRules : BaseBusinessRules
    where TFakeData : BaseFakeData<TEntity>, new()
{
    public IMapper Mapper;
    public Mock<TRepository> MockRepository;
    public TBusinessRules BusinessRules;

    public BaseMockRepository(TFakeData fakeData)
    {
        MapperConfiguration mapperConfig =
            new(c =>
            {
                c.AddProfile<TMappingProfile>();
            });
        Mapper = mapperConfig.CreateMapper();

        MockRepository = MockRepositoryHelper.GetRepository<TRepository, TEntity>(fakeData.Data);
        BusinessRules = (TBusinessRules)Activator.CreateInstance(type: typeof(TBusinessRules), MockRepository.Object);
    }
}
