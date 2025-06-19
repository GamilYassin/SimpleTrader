using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.EntityFramework.Services.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.Data;

public class GenericDataService<T> : IDataService<T> where T : DomainObject
{
    private readonly AppDbContextFactory _contextFactory;
    private readonly NonQueryDataService<T> _nonQueryDataService;

    public GenericDataService(AppDbContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
        _nonQueryDataService = new NonQueryDataService<T>(contextFactory);
    }

    public async Task<T> Create(T entity)
    {
        return await _nonQueryDataService.Create(entity);
    }

    public async Task<bool> Delete(int id)
    {
        return await _nonQueryDataService.Delete(id);
    }

    public async Task<T> Get(int id)
    {
        using(AppDbContext context = _contextFactory.CreateDbContext())
        {
            T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
            return entity;
        }
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        using(AppDbContext context = _contextFactory.CreateDbContext())
        {
            IEnumerable<T> entities = await context.Set<T>().ToListAsync();
            return entities;
        }
    }

    public async Task<T> Update(int id, T entity)
    {
        return await _nonQueryDataService.Update(id, entity);
    }
}