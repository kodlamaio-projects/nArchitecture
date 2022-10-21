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
    protected TContext Context { get; }

    public EfRepositoryBase(TContext context)
    {
        Context = context;
    }
    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        var query =  Context.Set<TEntity>().AsQueryable();
        if (include != null) query = include(query);
        return await query.FirstOrDefaultAsync(predicate);
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

    public async Task<IPaginate<TEntity>> GetListByDynamicAsync(Dynamic.Dynamic dynamic,
                                                                Func<IQueryable<TEntity>,
                                                                        IIncludableQueryable<TEntity, object>>?
                                                                    include = null,
                                                                int index = 0, int size = 10,
                                                                bool enableTracking = true,
                                                                CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Query().AsQueryable().ToDynamic(dynamic);
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        return await queryable.ToPaginateAsync(index, size, 0, cancellationToken);
    }

    public IQueryable<TEntity> Query()
    {
        return Context.Set<TEntity>();
    }

    public async Task<TEntity> AddAsync(TEntity entity, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        Context.Entry(entity).State = EntityState.Added;
        await Context.SaveChangesAsync();

        TEntity addedEntity = await GetAsync(e => e.Id == entity.Id, include: include);

        return addedEntity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync();

        TEntity updatedEntity = await GetAsync(e => e.Id == entity.Id, include: include);

        return updatedEntity;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        Context.Entry(entity).State = EntityState.Deleted;

        TEntity deletedEntity = await GetAsync(e => e.Id == entity.Id, include: include);

        await Context.SaveChangesAsync();

        return deletedEntity;
    }

    public TEntity Get(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        var query = Context.Set<TEntity>().AsQueryable();
        if (include != null) query = include(query);
        return query.FirstOrDefault(predicate);
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

    public IPaginate<TEntity> GetListByDynamic(Dynamic.Dynamic dynamic,
                                               Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>?
                                                   include = null, int index = 0, int size = 10,
                                               bool enableTracking = true)
    {
        IQueryable<TEntity> queryable = Query().AsQueryable().ToDynamic(dynamic);
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        return queryable.ToPaginate(index, size);
    }

    public TEntity Add(TEntity entity, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        Context.Entry(entity).State = EntityState.Added;
        Context.SaveChanges();

        TEntity addedEntity = Get(e => e.Id == entity.Id, include: include);

        return addedEntity;
    }

    public TEntity Update(TEntity entity, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        Context.Entry(entity).State = EntityState.Modified;
        Context.SaveChanges();

        TEntity updatedEntity = Get(e => e.Id == entity.Id, include: include);

        return updatedEntity;
    }

    public TEntity Delete(TEntity entity, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        Context.Entry(entity).State = EntityState.Deleted;

        TEntity deletedEntity = Get(e => e.Id == entity.Id, include: include);

        Context.SaveChanges();

        return deletedEntity;
    }

    
}