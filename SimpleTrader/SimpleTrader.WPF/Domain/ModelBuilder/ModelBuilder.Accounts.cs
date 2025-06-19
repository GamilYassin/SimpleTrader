using SimpleTrader.WPF.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace SimpleTrader.WPF.Domain.ModelBuilder;

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