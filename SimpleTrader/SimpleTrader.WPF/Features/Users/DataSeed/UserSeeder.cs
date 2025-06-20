#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FieldOps.Kernel.PasswordService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleTrader.WPF.Data;
using SimpleTrader.WPF.Features.Users.Models;

#endregion

namespace SimpleTrader.WPF.Features.Users.DataSeed;

public class UserSeeder(IServiceProvider service)
{
    private readonly ILogger<UserSeeder> _logger = service.GetRequiredService<ILogger<UserSeeder>>();
    private readonly IPasswordHasher _passwordHasher = service.GetRequiredService<IPasswordHasher>();

    private readonly IDbContextFactory<AppDbContext> _dbFactory =
        service.GetRequiredService<IDbContextFactory<AppDbContext>>();

    public async Task SeedAsync()
    {
        try
        {
            // 1. Check if data already exists
            await using var db = await _dbFactory.CreateDbContextAsync();
            var count = await db.Users.CountAsync();
            if (count > 0)
            {
                _logger.LogInformation("Seeding skipped for {TableName}: Table already contains data.",
                    nameof(User));
                return;
            }

            _logger.LogInformation("Seeding data for {TableName}...", nameof(User));

            // 2. Configure Bogus Faker
            var users = new List<User>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Email = "Gamil.Yassin@gmail.com",
                    Username = "Admin",
                    PasswordHash = _passwordHasher.HashPassword("admin"),
                    DatedJoined = DateTime.Now
                }
            };

            // 3. Insert Data
            await db.Users.AddRangeAsync(users);
            var insertedCount = await db.SaveChangesAsync();

            _logger.LogInformation("Successfully seeded {Count} records for {TableName}.", insertedCount,
                nameof(User));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error seeding data for {TableName}: {ErrorMessage}", nameof(User), ex.Message);
            // Important: Do not re-throw, allow SeederRunner to continue
        }
    }
}