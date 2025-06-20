using System.Threading.Tasks;
using FieldOps.Kernel.Functional;
using SimpleTrader.WPF.Data.Repositories;
using SimpleTrader.WPF.Features.Accounts.Models;
using SimpleTrader.WPF.Features.Users.Models;

namespace SimpleTrader.WPF.Features.Users.Services;

public interface IUserService : IRepository<User>
{
    Task<Validation<User>> GetByUserNameAsync(string username);
    Task<Validation<User>> GetByEmailAsync(string email);
    Task<bool> IsEmailExistsAsync(string email);
    Task<bool> IsUserNameExistsAsync(string username);

}