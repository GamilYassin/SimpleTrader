using System.Threading.Tasks;
using SimpleTrader.WPF.Domain.Models;

namespace SimpleTrader.WPF.Domain.Services;

public interface IAccountService : IDataService<Account>
{
    Task<Account> GetByUsername(string username);
    Task<Account> GetByEmail(string email);
}