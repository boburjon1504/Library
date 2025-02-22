﻿using Library.DataAccess.DataContext;
using Library.Models.Common.ForEntity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.DataAccess.Repositories;

public abstract class BaseRepository<TEntity>(AppDbContext dbContext) where TEntity : Auditable
{
    private DbSet<TEntity> dbSet = dbContext.Set<TEntity>();
    protected IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression = default!)
    {
        return expression is null ? dbSet.AsQueryable() : dbSet.Where(expression).AsQueryable();
    }

    protected async ValueTask<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var created = await dbSet.AddAsync(entity, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    protected async ValueTask<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        dbSet.Update(entity);

        await dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    protected async ValueTask<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.IsDeleted = true;

        await dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }
}
