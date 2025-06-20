using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using SimpleTrader.WPF.AppServices.Toast;
using SimpleTrader.WPF.Features.Accounts.Models;
using SimpleTrader.WPF.Features.Accounts.Services;
using SimpleTrader.WPF.Features.Users.Services;
using SimpleTrader.WPF.Resources.Messages;
using SimpleTrader.WPF.Resources.ViewModels;

namespace SimpleTrader.WPF.Features.Accounts.ViewModels;

public partial class AccountListViewModel : ViewModelBase
{
    private readonly IAccountService _accountService;
    private readonly IAuthenticationService _authenticationService;
    private readonly IToastService _toastService;
    
    [ObservableProperty]
    private ObservableCollection<Account> _userAccounts;
    [ObservableProperty]
    private Account? _selectedAccount;

    /// <inheritdoc/>
    public AccountListViewModel(IServiceProvider service) : base(service)
    {
        _accountService = service.GetRequiredService<IAccountService>();
        _authenticationService = service.GetRequiredService<IAuthenticationService>();
        _toastService = service.GetRequiredService<IToastService>();
        
        Messenger.RegisterAll(this);
    }

    public override async Task InitializeAsync()
    {
        await LoadCollection();
    }

    public async void Receive(AccountCreatedMessage message)
    {
        await LoadCollection();
    }

    private async Task LoadCollection()
    {
        var accounts = await _accountService.GetAccountsByUserAsync(_authenticationService.CurrentUser);
        UserAccounts = new ObservableCollection<Account>(accounts);
    }
    
    [RelayCommand]
    private async void SelectAccount(Account? account)
    {
        if (SelectedAccount == null)
        {
            await _toastService.WarningAsync("No selected account");
            return;
        }
        
        _accountService.CurrentAccount = account;
        await _toastService.SuccessAsync($"New Account has been selected: {account!.Name}");
        Messenger.Send(new AccountSelectionChangedMessage(account!));
    }
}