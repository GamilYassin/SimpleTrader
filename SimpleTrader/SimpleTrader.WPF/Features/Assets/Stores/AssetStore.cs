using System;
using System.Collections.Generic;
using SimpleTrader.WPF.Features.Accounts.Stores;
using SimpleTrader.WPF.Features.Assets.Models;

namespace SimpleTrader.WPF.Features.Assets.Stores;

public class AssetStore
{
    private readonly IAccountStore _accountStore;

    public double AccountBalance => _accountStore.CurrentAccount?.Balance ?? 0;
    public IEnumerable<AssetTransaction> AssetTransactions => _accountStore.CurrentAccount?.AssetTransactions ?? new List<AssetTransaction>();

    public event Action StateChanged;

    public AssetStore(IAccountStore accountStore)
    {
        _accountStore = accountStore;

        _accountStore.StateChanged += OnStateChanged;
    }

    private void OnStateChanged()
    {
        StateChanged?.Invoke();
    }
}