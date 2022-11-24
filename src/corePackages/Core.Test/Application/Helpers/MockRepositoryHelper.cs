using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;

namespace Core.Test.Application.Helpers
{
    public static class MockRepositoryHelper
    {
        public static Mock<TRepository> GetRepository<TRepository, TEntity>(List<TEntity> list)
            where TEntity : Entity, new()
            where TRepository : class, IAsyncRepository<TEntity>, IRepository<TEntity>
        {
            Mock<TRepository> mockRepo = new Mock<TRepository>();

            Build(mockRepo, list);
            return mockRepo;
        }

        static void Build<TRepository, TEntity>(Mock<TRepository> mockRepo, List<TEntity> entityList)
            where TEntity : Entity, new()
            where TRepository : class, IAsyncRepository<TEntity>, IRepository<TEntity>
        {
            SetupGetListAsync(mockRepo, entityList);
            SetupGetAsync(mockRepo, entityList);
            SetupAddAsync(mockRepo, entityList);
            SetupUpdateAsync(mockRepo, entityList);
            SetupDeleteAsync(mockRepo, entityList);
        }
        static void SetupGetListAsync<TRepository, TEntity>(Mock<TRepository> mockRepo, List<TEntity> entityList)
            where TEntity : Entity, new()
            where TRepository : class, IAsyncRepository<TEntity>, IRepository<TEntity>
        {
            mockRepo.Setup(s => s.GetListAsync(It.IsAny<Expression<Func<TEntity, bool>>>(),
                 It.IsAny<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>>(),
                 It.IsAny<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>(),
                 It.IsAny<int>(),
                 It.IsAny<int>(),
                 It.IsAny<bool>(),
                 It.IsAny<CancellationToken>()))
          .ReturnsAsync((Expression<Func<TEntity, bool>> expression,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, int index, int size, bool enableTracking, CancellationToken cancellationToken
          ) =>
          {
              IList<TEntity> list = new List<TEntity>();

              if (expression == null)
              {
                  list = entityList;
              }
              else
              {
                  list = entityList.Where(expression.Compile()).ToList();
              }

              Paginate<TEntity> paginateList = new()
              {
                  Items = list

              };
              return paginateList;
          });
        }

        static void SetupGetAsync<TRepository, TEntity>(Mock<TRepository> mockRepo, List<TEntity> entityList)
            where TEntity : Entity, new()
            where TRepository : class, IAsyncRepository<TEntity>, IRepository<TEntity>
        {
            mockRepo.Setup(s => s.GetAsync(It.IsAny<Expression<Func<TEntity, bool>>>(),
                                           It.IsAny<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>(),
                                           It.IsAny<bool>(),
                                           It.IsAny<CancellationToken>()
                           ))
                    .ReturnsAsync((Expression<Func<TEntity, bool>> expression,
                                   Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
                                   bool enableTracking, CancellationToken cancellationToken) =>
                    {
                        TEntity? result = entityList.FirstOrDefault(predicate: expression.Compile());
                        return result;
                    });
        }

        static void SetupAddAsync<TRepository, TEntity>(Mock<TRepository> mockRepo, List<TEntity> entityList)
            where TEntity : Entity, new()
            where TRepository : class, IAsyncRepository<TEntity>, IRepository<TEntity>
        {
            mockRepo.Setup(r => r.AddAsync(It.IsAny<TEntity>())).ReturnsAsync((TEntity entity) =>
            {
                entityList.Add(entity);
                return entity;
            });
        }

        static void SetupUpdateAsync<TRepository, TEntity>(Mock<TRepository> mockRepo, List<TEntity> entityList)
            where TEntity : Entity, new()
            where TRepository : class, IAsyncRepository<TEntity>, IRepository<TEntity>
        {
            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<TEntity>())).ReturnsAsync((TEntity entity) =>
            {
                TEntity? result = entityList.FirstOrDefault(x => x.Id == entity.Id);
                if (result != null) result = entity;
                return result;
            });
        }

        static void SetupDeleteAsync<TRepository, TEntity>(Mock<TRepository> mockRepo, List<TEntity> entityList)
            where TEntity : Entity, new()
            where TRepository : class, IAsyncRepository<TEntity>, IRepository<TEntity>
        {
            mockRepo.Setup(r => r.DeleteAsync(It.IsAny<TEntity>())).ReturnsAsync((TEntity entity) =>
            {
                entityList.Remove(entity);
                return entity;
            });
        }
    }
}
