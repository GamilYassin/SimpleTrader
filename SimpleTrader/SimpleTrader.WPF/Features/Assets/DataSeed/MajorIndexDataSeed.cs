using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleTrader.WPF.Data;
using SimpleTrader.WPF.Features.Assets.Models;

namespace SimpleTrader.WPF.Features.Assets.DataSeed;

public class MajorIndexSeeder(IServiceProvider service)
{
    private readonly ILogger<MajorIndexSeeder> _logger = service.GetRequiredService<ILogger<MajorIndexSeeder>>();

    private readonly IDbContextFactory<AppDbContext> _dbFactory =
        service.GetRequiredService<IDbContextFactory<AppDbContext>>();

    public async Task SeedAsync()
    {
        try
        {
            // 1. Check if data already exists
            await using var db = await _dbFactory.CreateDbContextAsync();
            var count = await db.MajorIndices.CountAsync();
            if (count > 0)
            {
                _logger.LogInformation("Seeding skipped for {TableName}: Table already contains data.",
                    nameof(MajorIndex));
                return;
            }

            _logger.LogInformation("Seeding data for {TableName}...", nameof(MajorIndex));

            // 2. Configure Bogus Faker
            var indecies = new List<MajorIndex>()
            {
                new MajorIndex
                {
                    Id = Guid.NewGuid(),
                    IndexName = "S&P 500",
                    UriSuffix = "^GSPC",
                    Price = 5300.00m,
                    Changes = 12.34m
                },
                new MajorIndex
                {
                    Id = Guid.NewGuid(),
                    IndexName = "NASDAQ Composite",
                    UriSuffix = "^IXIC",
                    Price = 17400.00m,
                    Changes = -45.67m
                },
                new MajorIndex
                {
                    Id = Guid.NewGuid(),
                    IndexName = "Dow Jones Industrial Average",
                    UriSuffix = "^DJI",
                    Price = 38800.00m,
                    Changes = 89.10m
                },
                new MajorIndex
                {
                    Id = Guid.NewGuid(),
                    IndexName = "Russell 2000",
                    UriSuffix = "^RUT",
                    Price = 2000.00m,
                    Changes = 5.50m
                },
                new MajorIndex
                {
                    Id = Guid.NewGuid(),
                    IndexName = "FTSE 100",
                    UriSuffix = "^FTSE",
                    Price = 8200.00m,
                    Changes = -30.25m
                }
            };            

            // 3. Insert Data
            await db.MajorIndices.AddRangeAsync(indecies);
            var insertedCount = await db.SaveChangesAsync();

            _logger.LogInformation("Successfully seeded {Count} records for {TableName}.", insertedCount,
                nameof(MajorIndex));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error seeding data for {TableName}: {ErrorMessage}", nameof(MajorIndex), ex.Message);
            // Important: Do not re-throw, allow SeederRunner to continue
        }
    }
}
