using SimpleTrader.WPF.Domain.Models;

namespace SimpleTrader.WPF.Domain.ModelBuilder;

public static partial class ModelBuilderExtensions
{
    public static Microsoft.EntityFrameworkCore.ModelBuilder ConfigureUsers(this Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(c => c.Username)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(c => c.PasswordHash)
                .IsRequired()
                .HasMaxLength(20);
            
            entity.Property(c => c.CreatedAt)
                .ValueGeneratedOnAdd();
            
            entity.Property(c => c.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate();
            
            // Indexes
            entity.HasIndex(c => c.Username)
                .IsUnique();
            entity.HasIndex(c => c.Email)
                .IsUnique();
        });
        
        return modelBuilder;
    }
}