using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleTrader.WPF.Data.Services.Common;
using SimpleTrader.WPF.Domain.Models;
using SimpleTrader.WPF.Domain.Services;

namespace SimpleTrader.WPF.Data.Services;

public class AccountDataService : IAccountService
{
    private readonly AppDbContextFactory _contextFactory;
    private readonly NonQueryDataService<Account> _nonQueryDataService;

    public AccountDataService(AppDbContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
        _nonQueryDataService = new NonQueryDataService<Account>(contextFactory);
    }

    public async Task<Account> CreateAsync(Account entity)
    {
        return await _nonQueryDataService.Create(entity);
    }

    public async Task<bool> Delete(int id)
    {
        return await _nonQueryDataService.Delete(id);
    }

    public async Task<Account> GetById(int id)
    {
        await using (AppDbContext context = _contextFactory.CreateDbContext())
        {
            Account entity = await context.Accounts
                .Include(a => a.AccountHolder)
                .Include(a => a.AssetTransactions)
                .FirstOrDefaultAsync((e) => e.Id == id);
            return entity;
        }
    }

    public async Task<IEnumerable<Account>> GetAllAsync()
    {
        await using (AppDbContext context = _contextFactory.CreateDbContext())
        {
            IEnumerable<Account> entities = await context.Accounts
                .Include(a => a.AccountHolder)
                .Include(a => a.AssetTransactions)
                .ToListAsync();
            return entities;
        }
    }

    public async Task<Account> GetByEmail(string email)
    {
        await using (AppDbContext context = _contextFactory.CreateDbContext())
        {
            return await context.Accounts
                .Include(a => a.AccountHolder)
                .Include(a => a.AssetTransactions)
                .FirstOrDefaultAsync(a => a.AccountHolder.Email == email);
        }
    }

    public async Task<Account?> GetByUsername(string username)
    {
        await using (AppDbContext context = _contextFactory.CreateDbContext())
        {
            return await context.Accounts
                .Include(a => a.AccountHolder)
                .Include(a => a.AssetTransactions)
                .FirstOrDefaultAsync(a => a.AccountHolder.Username == username);
        }
    }

    public async Task<Account> Update(int id, Account entity)
    {
        return await _nonQueryDataService.Update(id, entity);
    }
}