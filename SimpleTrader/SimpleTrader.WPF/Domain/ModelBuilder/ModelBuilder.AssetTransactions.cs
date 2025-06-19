using SimpleTrader.WPF.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace SimpleTrader.WPF.Domain.ModelBuilder;

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