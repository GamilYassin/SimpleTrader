using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FieldOps.Kernel.Entities;
using Microsoft.EntityFrameworkCore;
using SimpleTrader.WPF.Data.Services.Common;

namespace SimpleTrader.WPF.Data.Repositories;

public class GenericRepository<T>(IDbContextFactory<AppDbContext> contextFactory) 
    : IRepository<T> where T : EntityBase
{
    private readonly NonQueryDataService<T> _nonQueryDataService = new NonQueryDataService<T>(contextFactory);

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

    public async Task<T?> UpdateAsync(Guid id, T? entity)
    {
        if (entity == null)
            return Invalid<T>("Entity is null");
        
        try
        {
            await using AppDbContext context = await ContextFactory.CreateDbContextAsync(cancellationToken);
            var query = context.Set<T>().AsQueryable();
            var existingEntity = await query.FirstOrDefaultAsync(x => x.Id == entity.Id, cancellationToken: cancellationToken);

            if (existingEntity is null)
            {
                return Invalid<T>("Entity Has not been found");
            }

            context.Entry(existingEntity).CurrentValues.SetValues(entity);

            var result = await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        
            return result > 0
                ? Valid(existingEntity)
                : Invalid<T>("Entity  is found, however Update operation failed");
        }
        catch (Exception e)
        {
            return Invalid<T>($"Entity Update operation failed with Exception:\n{e}");
        }
        
        
        await using var context = await contextFactory.CreateDbContextAsync();
        entity.Id = id;

        context.Set<T>().Update(entity);
        await context.SaveChangesAsync();

        return entity;
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }



}