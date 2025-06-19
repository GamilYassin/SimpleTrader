using System;
using SimpleTrader.WPF.Features.Accounts.Models;

namespace SimpleTrader.WPF.Features.Accounts.Stores;

public class AccountStore : IAccountStore
{
    private Account? _currentAccount;
    public Account? CurrentAccount
    {
        get => _currentAccount;
        set
        {
            _currentAccount = value;
            StateChanged?.Invoke();
        }
    }

    public event Action? StateChanged;

}