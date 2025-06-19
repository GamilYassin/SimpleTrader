using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FieldOps.Kernel.Entities;
using Microsoft.EntityFrameworkCore;
using Throw;

namespace SimpleTrader.WPF.Data.Repositories;

public class GenericRepository<T>(IDbContextFactory<AppDbContext> contextFactory) 
    : IRepository<T> where T : EntityBase
{
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        return await context.Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<T?> GetByIdAsync(Guid id)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var entity = await context.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync((e) => e.Id == id);
        return entity;
    }

    public async Task<T> CreateAsync(T entity)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var createdResult = await context.Set<T>().AddAsync(entity);
        await context.SaveChangesAsync();

        return createdResult.Entity;
    }

    public async Task<T> UpdateAsync(Guid id, T? entity)
    {
        entity.ThrowIfNull();
        
        try
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            var query = context.Set<T>().AsQueryable();
            var existingEntity = await query.FirstOrDefaultAsync(x => x.Id == entity.Id);

            existingEntity.ThrowIfNull("Entity Has not been found");

            context.Entry(existingEntity).CurrentValues.SetValues(entity);

            await context.SaveChangesAsync();
        
            return existingEntity;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Entity Update operation failed with Exception:\n{e}");
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            await using AppDbContext dbContext = await contextFactory.CreateDbContextAsync();
            var entity = await dbContext.Set<T>()
                .FirstOrDefaultAsync(x => x.Id == id);
            
            entity.ThrowIfNull("Entity Has not been found");
                
            dbContext.Set<T>().Remove(entity);
            var count = await dbContext.SaveChangesAsync().ConfigureAwait(false);
            return count > 0
                ? true
                : false;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Entity Delete operation failed with Exception:\n{e}");
        }
    }
}