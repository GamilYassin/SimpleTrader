using Microsoft.EntityFrameworkCore;
using SimpleTrader.WPF.Domain.Models;

namespace SimpleTrader.WPF.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AssetTransaction> AssetTransactions { get; set; }
    public AppDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssetTransaction>()
            .OwnsOne(a => a.Asset);

        base.OnModelCreating(modelBuilder);
    }
}