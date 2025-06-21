using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using SimpleTrader.WPF.Features.Accounts.Services;
using SimpleTrader.WPF.Features.Assets.Models;
using SimpleTrader.WPF.Features.Assets.Services;
using SimpleTrader.WPF.Features.Users.Services;
using SimpleTrader.WPF.Resources.ViewModels;

namespace SimpleTrader.WPF.Features.Assets.ViewModels;

public partial class BuyViewModel(IServiceProvider service) : ViewModelBase(service)
{
    private readonly IAssetService _assetService = service.GetRequiredService<IAssetService>();
    private readonly IAuthenticationService _authenticationService = service.GetRequiredService<IAuthenticationService>();
    private readonly IAccountService _accountService = service.GetRequiredService<IAccountService>();

    [ObservableProperty] private ObservableCollection<string> _assetsSuggestion = [];
    [ObservableProperty] private string? _searchText;
    [ObservableProperty] private Asset? _selectedAsset;
    
    [ObservableProperty] 
    [CustomValidation(typeof(BuyViewModel), nameof(ValidateBuyQunatity))]
    private string? _buyQuantity;
    
    // Visibility
    [ObservableProperty] private bool _IsCardVisible = false;
    [ObservableProperty] private bool _IsHelpTextVisible = true;
    
    public override Task InitializeAsync()
    {
        // var assetsValues = await _assetService.GetAssetsSearchSuggestions();
        AssetsSuggestion = [];
        return Task.CompletedTask;
    }

    [RelayCommand]
    private async Task SearchAssets()
    {
        var asset = await _assetService.FilterAssets(SearchText);
        SelectedAsset = asset.IsValid 
            ? asset.Value 
            : null;

        UpdateVisibilityFlags();
    }

    private void UpdateVisibilityFlags()
    {
        if (_accountService.CurrentAccount == null)
        {
            IsCardVisible = false;
        }
        else
        {
            IsCardVisible = SelectedAsset != null;
        }
        IsHelpTextVisible = !IsCardVisible;
    }

    [RelayCommand]
    private void ClearSearch()
    {
        SearchText = null;
        SelectedAsset = null;
        UpdateVisibilityFlags();
    }
    
    [RelayCommand]
    private async Task BuyAsset()
    {
        if(!ValidateAll()) return;
        if(_accountService.CurrentAccount == null) return;
        if (SelectedAsset == null) return;
        
        if(!int.TryParse(BuyQuantity, out var buyQty))
        {
            await ToastService.WarningAsync("Invalid Quantity to Buy.");
            return;
        }

        var transaction = new AssetTransaction()
        {
            Id = Guid.NewGuid(),
            Symbol = SelectedAsset.Symbol,
            PricePerShare = SelectedAsset.PricePerShare,
            AccountId = _accountService.CurrentAccount.Id,
            DateProcessed = DateTime.UtcNow,
            IsPurchase = true,
            Shares = buyQty,
        };
        
        var result = await _accountService.ExecuteBuyTransactionAsync(transaction);
        await ToastService.ShowResultAsync(result,
            "Asset Buy Operation",
            $"Asset Buy Operation for {transaction.Symbol} with {transaction.PricePerShare} price/share for {transaction.Shares} is Successful.");
    }
    
    public static ValidationResult ValidateBuyQunatity(string quantity, ValidationContext context)
    {
        if (string.IsNullOrWhiteSpace(quantity))
            return new ValidationResult("Buy Quantity is required.");

        return !int.TryParse(quantity, out var balance) 
            ? new ValidationResult("Invalid Buy Quantity format.") 
            : ValidationResult.Success!;
    }

    private bool CanSearch()
    {
        return !string.IsNullOrWhiteSpace(SearchText);
    }
    
    // private bool CanBuy()
    // {
    //     return ValidateAll();
    // }
}