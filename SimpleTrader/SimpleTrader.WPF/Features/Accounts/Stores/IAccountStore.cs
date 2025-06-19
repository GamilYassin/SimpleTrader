using System;
using SimpleTrader.WPF.Features.Accounts.Models;

namespace SimpleTrader.WPF.Features.Accounts.Stores;

public interface IAccountStore
{
    Account? CurrentAccount { get; set; }
    event Action StateChanged;
}