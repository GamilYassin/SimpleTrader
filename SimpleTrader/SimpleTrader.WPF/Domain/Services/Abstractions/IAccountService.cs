using System.Threading.Tasks;
using SimpleTrader.WPF.Data.Repositories;
using SimpleTrader.WPF.Domain.Models;

namespace SimpleTrader.WPF.Domain.Services.Abstractions;

public interface IAccountService : IRepository<Account>
{
    Task<Account?> GetByUserNameAsync(string username);
    Task<Account?> GetByEmailAsync(string email);
    Task<bool> IsEmailExistsAsync(string email);
    Task<bool> IsUserNameExistsAsync(string username);
}