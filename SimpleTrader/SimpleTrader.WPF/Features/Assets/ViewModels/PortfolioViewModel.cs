using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using SimpleTrader.WPF.Features.Accounts.Services;
using SimpleTrader.WPF.Features.Assets.DTOs;
using SimpleTrader.WPF.Features.Assets.Models;
using SimpleTrader.WPF.Features.Users.Services;
using SimpleTrader.WPF.Resources.Messages;
using SimpleTrader.WPF.Resources.ViewModels;

namespace SimpleTrader.WPF.Features.Assets.ViewModels;

public partial class PortfolioViewModel : ViewModelBase
{
    private readonly IAccountService _accountService;
    private readonly IAuthenticationService  _authenticationService;
    
    [ObservableProperty]
    private ObservableCollection<AccountAssetDto> _assets;
    [ObservableProperty]
    private AccountAssetDto? _selectedAsset;
    [ObservableProperty] 
    private bool _isAccountSelected = false;
    [ObservableProperty] 
    private bool _isAccountNotSelected = true;

    /// <inheritdoc/>
    public PortfolioViewModel(IServiceProvider service) : base(service)
    {
        _accountService = service.GetRequiredService<IAccountService>();
        _authenticationService = service.GetRequiredService<IAuthenticationService>();
        
        Messenger.Register<AccountSelectionChangedMessage>(this , (r, m) => UpdateAccount() );
    }

    private void UpdateAccount()
    {
        IsAccountSelected = _accountService.CurrentAccount != null;
        IsAccountNotSelected = !IsAccountSelected;
    }

    public override async Task InitializeAsync()
    {
        var accountAssets = await _accountService.GetAccountAssetsAsync(_accountService.CurrentAccount);
        Assets = new ObservableCollection<AccountAssetDto>(accountAssets);
    }
}