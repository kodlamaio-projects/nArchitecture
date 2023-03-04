using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Core.Persistence.Dynamic;

namespace Core.Persistence.Repositories;

public interface IRepository<T> : IQuery<T> where T : Entity
{
    T Get(Expression<Func<T, bool>> predicate, Func<IQueryable<T>,
          IIncludableQueryable<T, object>>? include = null, bool enableTracking = true);

    IPaginate<T> GetList(Expression<Func<T, bool>>? predicate = null,
                         Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                         Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                         int index = 0, int size = 10,
                         bool enableTracking = true);

    IPaginate<T> GetListByDynamic(DynamicQuery dynamicQuery,
                                  Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                  int index = 0, int size = 10, bool enableTracking = true);

    T Add(T entity);
    IList<T> AddRange(IList<T> entities);
    T Update(T entity);
    IList<T> UpdateRange(IList<T> entities);
    T Delete(T entity);
    IList<T> DeleteRange(IList<T> entities);
}