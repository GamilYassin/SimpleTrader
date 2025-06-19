using SimpleTrader.WPF.Features.Assets.Models;

namespace SimpleTrader.WPF.Features.Assets.ModelBuilder;

public static partial class ModelBuilderExtensions
{
    public static Microsoft.EntityFrameworkCore.ModelBuilder ConfigureMajorIndecies(this Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MajorIndex>(entity =>
        {
            entity.Property(c => c.IndexName)
                .IsRequired()
                .HasMaxLength(20);
            
            entity.Property(c => c.Type)
                .HasConversion<string>();
            
            entity.Property(c => c.CreatedAt)
                .ValueGeneratedOnAdd();
            
            entity.Property(c => c.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate();
            
            // Indexes
            entity.HasIndex(c => c.IndexName)
                .IsUnique();
        });
        
        return modelBuilder;
    }
}