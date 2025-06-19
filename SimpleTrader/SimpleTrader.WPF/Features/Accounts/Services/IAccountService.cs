using System.Threading.Tasks;
using SimpleTrader.WPF.Data.Repositories;
using SimpleTrader.WPF.Features.Accounts.Models;

namespace SimpleTrader.WPF.Features.Accounts.Services;

public interface IAccountService : IRepository<Account>
{
    Task<Account?> GetByUserNameAsync(string username);
    Task<Account?> GetByEmailAsync(string email);
    Task<bool> IsEmailExistsAsync(string email);
    Task<bool> IsUserNameExistsAsync(string username);
}