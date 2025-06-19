using Microsoft.EntityFrameworkCore;
using SimpleTrader.WPF.Features.Assets.Models;

namespace SimpleTrader.WPF.Features.Assets.ModelBuilder;

public static partial class ModelBuilderExtensions
{
    public static Microsoft.EntityFrameworkCore.ModelBuilder ConfigureAssetTransactions(this Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssetTransaction>(entity =>
        {
            entity.Property(c => c.CreatedAt)
                .ValueGeneratedOnAdd();
            
            entity.Property(c => c.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate();
            
            entity.ComplexProperty(x => x.Asset)
                .IsRequired();

            entity.HasOne(c => c.Account)
                .WithMany(x => x.AssetTransactions)
                .HasForeignKey(c => c.AccountId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        return modelBuilder;
    }
}