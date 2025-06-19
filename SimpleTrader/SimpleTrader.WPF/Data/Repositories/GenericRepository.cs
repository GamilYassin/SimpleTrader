using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FieldOps.Kernel.Entities;
using FieldOps.Kernel.Functional;
using Microsoft.EntityFrameworkCore;
using Throw;

namespace SimpleTrader.WPF.Data.Repositories;

public class GenericRepository<T>(IDbContextFactory<AppDbContext> contextFactory) 
    : IRepository<T> where T : EntityBase
{
    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        return await context.Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }
    
    public virtual async Task<Validation<T>> GetByIdAsync(Guid id)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var entity = await context.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync((e) => e.Id == id);
        return entity != null 
            ? Valid(entity) 
            : Invalid<T>("Entity with passed Id is not found");
    }

    public virtual async Task<Validation<T>> CreateAsync(T entity)
    {
        try
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            var createdResult = await context.Set<T>().AddAsync(entity);
            var count = await context.SaveChangesAsync();

            return count > 0
                ? Valid(entity)
                : Invalid<T>("Entity Create Operation is not Successful");
        }
        catch (Exception e)
        {
            return Invalid<T>("Entity Create Operation is not Successful with Exception:\n" + e);
        }
    }

    public virtual async Task<Validation<T>> UpdateAsync(Guid id, T? entity)
    {
        try
        {
            if (entity == null)
                return Invalid<T>("Entity is null, can not update");
            
            await using var context = await contextFactory.CreateDbContextAsync();
            var query = context.Set<T>().AsQueryable();
            var existingEntity = await query.FirstOrDefaultAsync(x => x.Id == entity.Id);
            
            if (existingEntity == null)
                return Invalid<T>("Entity with passed Id is not found");

            context.Entry(existingEntity).CurrentValues.SetValues(entity);

            var count = await context.SaveChangesAsync();
        
            return count > 0
                ? Valid(entity)
                : Invalid<T>("Entity Update Operation is not Successful");;
        }
        catch (Exception e)
        {
            return Invalid<T>("Entity Update Operation is not Successful with Exception:\n" + e);
        }
    }

    public virtual async Task<Validation<bool>> DeleteAsync(Guid id)
    {
        try
        {
            await using var dbContext = await contextFactory.CreateDbContextAsync();
            var entity = await dbContext.Set<T>()
                .FirstOrDefaultAsync(x => x.Id == id);
            
            entity.ThrowIfNull("Entity Has not been found");
                
            dbContext.Set<T>().Remove(entity);
            var count = await dbContext.SaveChangesAsync().ConfigureAwait(false);
            return count > 0
                ? Valid(true)
                : Valid(false);
        }
        catch (Exception e)
        {
            return Invalid<bool>($"Entity Delete operation failed with Exception:\n{e}");
        }
    }
}