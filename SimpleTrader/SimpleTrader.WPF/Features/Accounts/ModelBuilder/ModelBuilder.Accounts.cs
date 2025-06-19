using Microsoft.EntityFrameworkCore;
using SimpleTrader.WPF.Features.Accounts.Models;

namespace SimpleTrader.WPF.Features.Accounts.ModelBuilder;

public static partial class ModelBuilderExtensions
{
    public static Microsoft.EntityFrameworkCore.ModelBuilder ConfigureAccounts(this Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.Property(c => c.CreatedAt)
                .ValueGeneratedOnAdd();
            
            entity.Property(c => c.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate();

            entity.HasOne(c => c.AccountHolder)
                .WithMany(x => x.UserAccounts)
                .HasForeignKey(c => c.AccountHolderId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        return modelBuilder;
    }
}