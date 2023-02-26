using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Persistence.Repositories;

public class EfRepositoryBase<TEntity, TContext> : IAsyncRepository<TEntity>, IRepository<TEntity>
    where TEntity : Entity
    where TContext : DbContext
{
    protected TContext Context { get; }

    public EfRepositoryBase(TContext context)
    {
        Context = context;
    }

    public IQueryable<TEntity> Query()
    {
        return Context.Set<TEntity>();
    }

    public TEntity Get(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>,
                       IIncludableQueryable<TEntity, object>>? include = null, bool enableTracking = true)
    {
        IQueryable<TEntity> queryable = Query().AsQueryable();
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        return queryable.FirstOrDefault(predicate);
    }
    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>,
                                             IIncludableQueryable<TEntity, object>>? include = null, bool enableTracking = true,
                                         CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Query().AsQueryable();
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
    }
    public IPaginate<TEntity> GetList(Expression<Func<TEntity, bool>>? predicate = null,
                                      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                      Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                      int index = 0, int size = 10,
                                      bool enableTracking = true)
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        if (predicate != null) queryable = queryable.Where(predicate);
        if (orderBy != null)
            return orderBy(queryable).ToPaginate(index, size);
        return queryable.ToPaginate(index, size);
    }

    public async Task<IPaginate<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                                       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy =
                                                           null,
                                                       Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>?
                                                           include = null,
                                                       int index = 0, int size = 10, bool enableTracking = true,
                                                       CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        if (predicate != null) queryable = queryable.Where(predicate);
        if (orderBy != null)
            return await orderBy(queryable).ToPaginateAsync(index, size, 0, cancellationToken);
        return await queryable.ToPaginateAsync(index, size, 0, cancellationToken);
    }
    public IPaginate<TEntity> GetListByDynamic(DynamicQuery dynamicQuery,
                                               Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>?
                                                   include = null, int index = 0, int size = 10,
                                               bool enableTracking = true)
    {
        IQueryable<TEntity> queryable = Query().AsQueryable().ToDynamic(dynamicQuery);
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        return queryable.ToPaginate(index, size);
    }

    public async Task<IPaginate<TEntity>> GetListByDynamicAsync(DynamicQuery dynamicQuery,
                                                                Func<IQueryable<TEntity>,
                                                                        IIncludableQueryable<TEntity, object>>?
                                                                    include = null,
                                                                int index = 0, int size = 10,
                                                                bool enableTracking = true,
                                                                CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Query().AsQueryable().ToDynamic(dynamicQuery);
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        return await queryable.ToPaginateAsync(index, size, 0, cancellationToken);
    }

    public TEntity Add(TEntity entity)
    {
        Context.Add(entity);
        Context.SaveChanges();
        return entity;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await Context.AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity;
    }
    public IList<TEntity> AddRange(IList<TEntity> entities)
    {
        Context.AddRange(entities);
        Context.SaveChanges();
        return entities;
    }
    public async Task<IList<TEntity>> AddRangeAsync(IList<TEntity> entityList)
    {
        await Context.AddRangeAsync(entityList);
        await Context.SaveChangesAsync();
        return entityList;
    }

    public TEntity Update(TEntity entity)
    {
        Context.Update(entity);
        Context.SaveChanges();
        return entity;
    }
    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        Context.Update(entity);
        await Context.SaveChangesAsync();
        return entity;
    }
    public IList<TEntity> UpdateRange(IList<TEntity> entities)
    {
        Context.UpdateRange(entities);
        Context.SaveChanges();
        return entities;
    }

    public async Task<IList<TEntity>> UpdateRangeAsync(IList<TEntity> entities)
    {
        Context.UpdateRange(entities);
        await Context.SaveChangesAsync();
        return entities;
    }

    public TEntity Delete(TEntity entity)
    {
        Context.Remove(entity);
        Context.SaveChanges();
        return entity;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        Context.Remove(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public IList<TEntity> DeleteRange(IList<TEntity> entities)
    {
        Context.RemoveRange(entities);
        Context.SaveChanges();
        return entities;
    }
    public async Task<IList<TEntity>> DeleteRangeAsync(IList<TEntity> entities)
    {
        Context.RemoveRange(entities);
        await Context.SaveChangesAsync();
        return entities;
    }
}