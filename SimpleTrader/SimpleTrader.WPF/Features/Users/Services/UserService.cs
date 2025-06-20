using System.Threading.Tasks;
using FieldOps.Kernel.Functional;
using Microsoft.EntityFrameworkCore;
using SimpleTrader.WPF.Data;
using SimpleTrader.WPF.Data.Repositories;
using SimpleTrader.WPF.Features.Users.Models;

namespace SimpleTrader.WPF.Features.Users.Services;

public class UserService(IDbContextFactory<AppDbContext> contextFactory)
    : GenericRepository<User>(contextFactory), IUserService
{
    public async Task<Validation<User>> GetByEmailAsync(string email)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var entity =  await context.Set<User>()
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email.ToLower() == email.ToLower());
        return entity.ToValidation();
    }

    public async Task<bool> IsEmailExistsAsync(string email)
    {
        var result = await GetByEmailAsync(email);
        return result.IsValid;
    }

    public async Task<bool> IsUserNameExistsAsync(string username)
    {
        var result = await GetByUserNameAsync(username);
        return result.IsValid;
    }
    
    public async Task<Validation<User>> GetByUserNameAsync(string username)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var entity =  await context.Set<User>()
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Username.ToLower() == username.ToLower());
        return entity.ToValidation();
    }
}