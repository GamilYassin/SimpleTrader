using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleTrader.WPF.Data;
using SimpleTrader.WPF.Features.Assets.Models;

namespace SimpleTrader.WPF.Features.Assets.DataSeed;

public class AssetSeeder(IServiceProvider service)
{
    private readonly ILogger<AssetSeeder> _logger = service.GetRequiredService<ILogger<AssetSeeder>>();

    private readonly IDbContextFactory<AppDbContext> _dbFactory =
        service.GetRequiredService<IDbContextFactory<AppDbContext>>();

    public async Task SeedAsync()
    {
        try
        {
            // 1. Check if data already exists
            await using AppDbContext db = await _dbFactory.CreateDbContextAsync();
            int count = await db.Assets.CountAsync();
            if (count > 0)
            {
                _logger.LogInformation("Seeding skipped for {TableName}: Table already contains data.",
                    nameof(Asset));
                return;
            }

            _logger.LogInformation("Seeding data for {TableName}...", nameof(Asset));

            // 2. Configure Bogus Faker
            var assets = new List<Asset>()
            {
                new Asset { Id = Guid.NewGuid(), Symbol = "AAPL", CompanyName = "Apple Inc." },
                new Asset { Id = Guid.NewGuid(), Symbol = "MSFT", CompanyName = "Microsoft Corporation" },
                new Asset { Id = Guid.NewGuid(), Symbol = "GOOGL", CompanyName = "Alphabet Inc." },
                new Asset { Id = Guid.NewGuid(), Symbol = "AMZN", CompanyName = "Amazon.com, Inc." },
                new Asset { Id = Guid.NewGuid(), Symbol = "TSLA", CompanyName = "Tesla, Inc." },
                new Asset { Id = Guid.NewGuid(), Symbol = "META", CompanyName = "Meta Platforms, Inc." },
                new Asset { Id = Guid.NewGuid(), Symbol = "NVDA", CompanyName = "NVIDIA Corporation" },
                new Asset { Id = Guid.NewGuid(), Symbol = "JPM", CompanyName = "JPMorgan Chase & Co." },
                new Asset { Id = Guid.NewGuid(), Symbol = "V", CompanyName = "Visa Inc." },
                new Asset { Id = Guid.NewGuid(), Symbol = "WMT", CompanyName = "Walmart Inc." }
            };            

            // 3. Insert Data
            await db.Assets.AddRangeAsync(assets);
            int insertedCount = await db.SaveChangesAsync();

            _logger.LogInformation("Successfully seeded {Count} records for {TableName}.", insertedCount,
                nameof(Asset));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error seeding data for {TableName}: {ErrorMessage}", nameof(Asset), ex.Message);
            // Important: Do not re-throw, allow SeederRunner to continue
        }
    }
}
