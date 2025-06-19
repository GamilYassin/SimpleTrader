using System.Threading.Tasks;
using SimpleTrader.WPF.Data.Repositories;
using SimpleTrader.WPF.Domain.Models;

namespace SimpleTrader.WPF.Domain.Services.Abstractions;

public interface IAccountService : IRepository<Account>
{
    Task<Account?> GetByUsername(string username);
    Task<Account> GetByEmail(string email);
}