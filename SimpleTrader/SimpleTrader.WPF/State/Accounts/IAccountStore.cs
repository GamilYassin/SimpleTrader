using System;
using SimpleTrader.WPF.Features.Accounts.Models;

namespace SimpleTrader.WPF.State.Accounts;

public interface IAccountStore
{
    Account? CurrentAccount { get; set; }
    event Action StateChanged;
}