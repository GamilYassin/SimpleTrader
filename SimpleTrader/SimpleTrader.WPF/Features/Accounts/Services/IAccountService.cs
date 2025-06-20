using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleTrader.WPF.Data.Repositories;
using SimpleTrader.WPF.Features.Accounts.Models;
using SimpleTrader.WPF.Features.Assets.DTOs;
using SimpleTrader.WPF.Features.Assets.Models;
using SimpleTrader.WPF.Features.Users.Models;

namespace SimpleTrader.WPF.Features.Accounts.Services;

public interface IAccountService : IRepository<Account>
{
    Account? CurrentAccount { get; set; }
    Task<int> GetSharesCountAsync(Account account, Asset asset);
    Task<IEnumerable<Account>> GetAccountsByUserAsync(User? user);
    bool IsAccountNameUnique(string name);
    Task<IEnumerable<AccountAssetDto>> GetAccountAssetsAsync(Account? account);
}