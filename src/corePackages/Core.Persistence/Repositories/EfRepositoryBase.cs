using System.Linq.Expressions;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.Persistence.Repositories;

public class EfRepositoryBase<TEntity, TContext> : IAsyncRepository<TEntity>, IRepository<TEntity>
    where TEntity : Entity
    where TContext : DbContext
{
    public EfRepositoryBase(TContext context)
    {
        Context = context;
    }

    protected TContext Context { get; }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Added;
        await Context.SaveChangesAsync();
        return entity;
    }

    public async Task<List<TEntity>> AddRangeAsync(List<TEntity> entityList)
    {
        await Context.Set<TEntity>().AddRangeAsync(entityList);
        await Context.SaveChangesAsync();
        return entityList;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync();
        return entity;
    }

    public async Task<List<TEntity>> UpdateRangeAsync(List<TEntity> entityList)
    {
        Context.Set<TEntity>().UpdateRange(entityList);
        await Context.SaveChangesAsync();
        return entityList;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Deleted;
        await Context.SaveChangesAsync();
        return entity;
    }

    public async Task<List<TEntity>> DeleteRangeAsync(List<TEntity> entityList)
    {
        Context.Set<TEntity>().RemoveRange(entityList);
        await Context.SaveChangesAsync();
        return entityList;
    }

    public IQueryable<TEntity> Query()
    {
        return Context.Set<TEntity>();
    }

    public async Task<IPaginate<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
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

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>,
            IIncludableQueryable<TEntity, object>>? include = null, bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<IPaginate<TEntity>> GetListByDynamicAsync(Dynamic.Dynamic dynamic,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0, int size = 10, bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Query().ToDynamic(dynamic);
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        if (predicate != null) queryable = queryable.Where(predicate);
        return await queryable.ToPaginateAsync(index, size, 0, cancellationToken);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null,
        bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Query();
        if (predicate is not null) queryable = queryable.Where(predicate);
        if (!enableTracking) queryable = queryable.AsNoTracking();
        return await queryable.AnyAsync(cancellationToken);
    }

    public TEntity Add(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Added;
        Context.SaveChanges();
        return entity;
    }

    public List<TEntity> AddRange(List<TEntity> entityList)
    {
        Context.Set<TEntity>().AddRange(entityList);
        Context.SaveChanges();
        return entityList;
    }

    public TEntity Update(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
        Context.SaveChanges();
        return entity;
    }

    public List<TEntity> UpdateRange(List<TEntity> entityList)
    {
        Context.Set<TEntity>().UpdateRange(entityList);
        Context.SaveChanges();
        return entityList;
    }

    public TEntity Delete(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Deleted;
        Context.SaveChanges();
        return entity;
    }

    public List<TEntity> DeleteRange(List<TEntity> entityList)
    {
        Context.Set<TEntity>().RemoveRange(entityList);
        Context.SaveChanges();
        return entityList;
    }

    public TEntity? Get(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool enableTracking = true)
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        return queryable.FirstOrDefault(predicate);
    }

    public IPaginate<TEntity> GetList(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0, int size = 10, bool enableTracking = true)
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        if (predicate != null) queryable = queryable.Where(predicate);
        if (orderBy != null)
            return orderBy(queryable).ToPaginate(index, size);
        return queryable.ToPaginate(index, size);
    }

    public IPaginate<TEntity> GetListByDynamic(Dynamic.Dynamic dynamic,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10,
        bool enableTracking = true)
    {
        IQueryable<TEntity> queryable = Query().ToDynamic(dynamic);
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        if (predicate != null) queryable = queryable.Where(predicate);
        return queryable.ToPaginate(index, size);
    }

    public bool Any(Expression<Func<TEntity, bool>>? predicate = null, bool enableTracking = true)
    {
        IQueryable<TEntity> queryable = Query();
        if (predicate is not null) queryable = queryable.Where(predicate);
        if (!enableTracking) queryable = queryable.AsNoTracking();
        return queryable.Any();
    }
}