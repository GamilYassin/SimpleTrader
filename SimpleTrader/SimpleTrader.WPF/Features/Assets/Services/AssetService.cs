using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FieldOps.Kernel.Functional;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleTrader.WPF.Data;
using SimpleTrader.WPF.Data.Repositories;
using SimpleTrader.WPF.Features.Assets.Models;
using SimpleTrader.WPF.Features.Financials.Services;

namespace SimpleTrader.WPF.Features.Assets.Services;

public class AssetService(IDbContextFactory<AppDbContext> contextFactory,
    IServiceProvider service)
    : GenericRepository<Asset>(contextFactory), IAssetService
{
    private readonly IStockPriceService  _stockPriceService = service.GetRequiredService<IStockPriceService>();
    
    public async Task<IEnumerable<string>> GetAssetsSearchSuggestions()
    {
        await using var context = await contextFactory.CreateDbContextAsync();

        return context.Assets
            .AsNoTracking()
            .Select(a => new[] { a.Symbol, a.CompanyName })
            .AsEnumerable()
            .SelectMany(x => x)
            .Distinct()
            .OrderBy(x => x);
//            .ToList();
    }

    public async Task<Validation<Asset>> FilterAssets(string? searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
            return Invalid<Asset>("Search text cannot be blank");

        var normalized = searchText.Trim().ToLowerInvariant();

        await using var context = await contextFactory.CreateDbContextAsync();

        var result = await context.Assets
            .AsNoTracking()
            .FirstOrDefaultAsync(a =>
                a.Symbol.ToLower().Contains(normalized) 
                || a.CompanyName.ToLower().Contains(normalized));

        if (result == null) return Invalid<Asset>("No Asset has been found based on search text");
        
        var asset = await _stockPriceService.UpdateAssetPriceAsync(result);
        return asset;
    }
}