using System.Threading.Tasks;
using FieldOps.Kernel.Functional;
using SimpleTrader.WPF.Data.Repositories;
using SimpleTrader.WPF.Features.Accounts.Models;
using SimpleTrader.WPF.Features.Assets.Models;

namespace SimpleTrader.WPF.Features.Accounts.Services;

public interface IAccountService : IRepository<Account>
{
    Account? CurrentAccount { get; set; }
    Task<int> GetSharesCountAsync(Account account, Asset asset);
}