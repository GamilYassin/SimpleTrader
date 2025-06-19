using System.Threading.Tasks;
using FieldOps.Kernel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SimpleTrader.WPF.Data.Services.Common;

public class NonQueryDataService<T> where T : EntityBase
{
    private readonly AppDbContextFactory _contextFactory;

    public NonQueryDataService(AppDbContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<T> Create(T entity)
    {

    }

    public async Task<T> Update(int id, T entity)
    {
        await using (AppDbContext context = _contextFactory.CreateDbContext())
        {

        }
    }

    public async Task<bool> Delete(int id)
    {
        await using (AppDbContext context = _contextFactory.CreateDbContext())
        {
            T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();

            return true;
        }
    }
}