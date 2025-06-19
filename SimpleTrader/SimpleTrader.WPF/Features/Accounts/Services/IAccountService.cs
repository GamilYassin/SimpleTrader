using System.Threading.Tasks;
using FieldOps.Kernel.Functional;
using SimpleTrader.WPF.Data.Repositories;
using SimpleTrader.WPF.Features.Accounts.Models;
using SimpleTrader.WPF.Features.Assets.Models;

namespace SimpleTrader.WPF.Features.Accounts.Services;

public interface IAccountService : IRepository<Account>
{
    Task<Validation<Account>> GetByUserNameAsync(string username);
    Task<Validation<Account>> GetByEmailAsync(string email);
    Task<bool> IsEmailExistsAsync(string email);
    Task<bool> IsUserNameExistsAsync(string username);
    Task<int> GetSharesCountAsync(Account account, Asset asset);
}