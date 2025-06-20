using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FieldOps.Kernel.Functional;
using Microsoft.EntityFrameworkCore;
using SimpleTrader.WPF.Data;
using SimpleTrader.WPF.Data.Repositories;
using SimpleTrader.WPF.Features.Accounts.Models;
using SimpleTrader.WPF.Features.Assets.Models;
using SimpleTrader.WPF.Features.Users.Models;

namespace SimpleTrader.WPF.Features.Accounts.Services;

public class AccountService(IDbContextFactory<AppDbContext> contextFactory)
    : GenericRepository<Account>(contextFactory), IAccountService
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory = contextFactory;

    public override async Task<Validation<Account>> GetByIdAsync(Guid id)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var entity =  await context.Set<Account>()
            .Include(x => x.AccountHolder)
            .Include(x => x.AssetTransactions)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
        return entity.ToValidation();
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

    public Account? CurrentAccount { get; set; }

    public async Task<int> GetSharesCountAsync(Account account, Asset asset)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Set<AssetTransaction>()
            .AsNoTracking()
            .Where(x => x.AccountId == account.Id && x.Symbol == asset.Symbol)
            .SumAsync(a => a.IsPurchase 
                ? a.Shares // If purchased => add to count otherwise reduce 
                : -a.Shares);
    }

    public async Task<IEnumerable<Account>> GetAccountsByUserAsync(User? user)
    {
        if (user == null)
            return [];
        
        await using var context = await _contextFactory.CreateDbContextAsync();
        var entities =  await context.Set<Account>()
            .Include(x => x.AccountHolder)
            .Include(x => x.AssetTransactions)
            .AsNoTracking()
            .Where(e => e.AccountHolderId == user.Id)
            .ToListAsync();
        return entities;
    }

    public bool IsAccountNameUnique(string name)
    {
        using var context = _contextFactory.CreateDbContext();
        var entity = context.Set<Account>()
            .AsNoTracking()
            .FirstOrDefault(e => e.Name.ToLower() == name.ToLower());
        return entity == null;
    }
}