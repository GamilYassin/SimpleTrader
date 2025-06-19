using System;
using FieldOps.Kernel.AppService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleTrader.WPF.Features.Accounts.ModelBuilder;
using SimpleTrader.WPF.Features.Accounts.Models;
using SimpleTrader.WPF.Features.Assets;
using SimpleTrader.WPF.Features.Assets.ModelBuilder;
using SimpleTrader.WPF.Features.Assets.Models;
using SimpleTrader.WPF.Features.Users.ModelBuilder;
using SimpleTrader.WPF.Features.Users.Models;

namespace SimpleTrader.WPF.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Account?> Accounts { get; set; }
    public DbSet<AssetTransaction> AssetTransactions { get; set; }
    // public AppDbContext(DbContextOptions options) : base(options) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(new ApplicationService().GetDefaultConnectString())
            .LogTo(Console.WriteLine, LogLevel.Information);
        base.OnConfiguring(options);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureUsers()
            .ConfigureAccounts()
            .ConfigureAssetTransactions()
            .ConfigureMajorIndecies();

        base.OnModelCreating(modelBuilder);
    }
}