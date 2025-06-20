using System;
using System.Globalization;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using SimpleTrader.WPF.Features.Accounts.Services;
using SimpleTrader.WPF.Features.Users.Services;
using SimpleTrader.WPF.Resources.Messages;
using SimpleTrader.WPF.Resources.ViewModels;

namespace SimpleTrader.WPF.Features.Home.ViewModels;

public partial class MainWindowViewModel : ViewModelBase //, IRecipient<LoggedInUserChangedMessage>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IAccountService _accountService;
    
    [ObservableProperty]
    private string? _userName =  string.Empty;
    
    public MainWindowViewModel(IServiceProvider service) : base(service)
    {
        _authenticationService = service.GetRequiredService<IAuthenticationService>();
        _accountService = service.GetRequiredService<IAccountService>();
        
        Messenger.Register<LoggedInUserChangedMessage>(this, (r, m) => SetUserName());
        Messenger.Register<AccountSelectionChangedMessage>(this, (r, m) => SetUserName());
    }

    public override Task InitializeAsync()
    {
       return Task.CompletedTask;
    }

    private void SetUserName()
    {
        var userName = _authenticationService.IsAuthenticated
            ? _authenticationService.CurrentUser!.Username
            : string.Empty;

        var accountName = _accountService.CurrentAccount == null 
            ? string.Empty 
            : $"[{_accountService.CurrentAccount.Name} - {_accountService.CurrentAccount.Balance.ToString("C", CultureInfo.GetCultureInfo("en-US"))}]";
        UserName = $"Welcome {userName} {accountName}";
    }
}