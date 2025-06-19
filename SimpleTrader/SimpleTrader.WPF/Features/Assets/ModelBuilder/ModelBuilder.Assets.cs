using SimpleTrader.WPF.Features.Assets.Models;

namespace SimpleTrader.WPF.Features.Assets.ModelBuilder;

public static partial class ModelBuilderExtensions
{
    public static Microsoft.EntityFrameworkCore.ModelBuilder ConfigureAssets(this Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>(entity =>
        {
            entity.Property(c => c.Symbol)
                .IsRequired()
                .HasMaxLength(20);
            
            entity.Property(c => c.CompanyName)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(c => c.CreatedAt)
                .ValueGeneratedOnAdd();
            
            entity.Property(c => c.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate();
        });
        
        return modelBuilder;
    }
}