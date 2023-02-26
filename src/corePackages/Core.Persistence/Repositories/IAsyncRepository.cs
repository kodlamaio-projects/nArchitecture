using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Persistence.Repositories;

public interface IAsyncRepository<T> : IQuery<T> where T : Entity
{
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>,
                      IIncludableQueryable<T, object>>? include = null, bool enableTracking = true,
                      CancellationToken cancellationToken = default);

    Task<IPaginate<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null,
                                    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                    Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                    int index = 0, int size = 10, bool enableTracking = true,
                                    CancellationToken cancellationToken = default);

    Task<IPaginate<T>> GetListByDynamicAsync(Dynamic.Dynamic dynamic,
                                             Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                             int index = 0, int size = 10, bool enableTracking = true,
                                             CancellationToken cancellationToken = default);

    Task<T> AddAsync(T entity);
    Task<IList<T>> AddRangeAsync(IList<T> entity);
    Task<T> UpdateAsync(T entity);
    Task<IList<T>> UpdateRangeAsync(IList<T> entity);
    Task<T> DeleteAsync(T entity);
    Task<IList<T>> DeleteRangeAsync(IList<T> entity);
}