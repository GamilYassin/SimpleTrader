using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleTrader.WPF.Data;
using SimpleTrader.WPF.Data.Repositories;
using SimpleTrader.WPF.Features.Accounts.Models;

namespace SimpleTrader.WPF.Features.Accounts.Services;

public class AccountService(IDbContextFactory<AppDbContext> contextFactory)
    : GenericRepository<Account>(contextFactory), IAccountService
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory = contextFactory;

    public override async Task<Account?> GetByIdAsync(Guid id)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var entity =  await context.Set<Account>()
            .Include(x => x.AccountHolder)
            .Include(x => x.AssetTransactions)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
        return entity;
    }

    public override async Task<IEnumerable<Account>> GetAllAsync()
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Set<Account>()
            .Include(x => x.AccountHolder)
            .Include(x => x.AssetTransactions)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Account?> GetByEmailAsync(string email)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var entity =  await context.Set<Account>()
            .Include(x => x.AccountHolder)
            .Include(x => x.AssetTransactions)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.AccountHolder.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        return entity;
    }

    public async Task<bool> IsEmailExistsAsync(string email)
    {
        var result = await GetByEmailAsync(email);
        return result != null;
    }

    public async Task<bool> IsUserNameExistsAsync(string username)
    {
        var result = await GetByUserNameAsync(username);
        return result != null;
    }

    public async Task<Account?> GetByUserNameAsync(string username)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var entity =  await context.Set<Account>()
            .Include(x => x.AccountHolder)
            .Include(x => x.AssetTransactions)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.AccountHolder.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        return entity;
    }
}