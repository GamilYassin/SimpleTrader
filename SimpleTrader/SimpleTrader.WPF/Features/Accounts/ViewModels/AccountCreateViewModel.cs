using System;
using System.ComponentModel.DataAnnotations;
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

public partial class AccountCreateViewModel(IServiceProvider service) : ViewModelBase(service)
{
    private readonly IAccountService _accountService = service.GetRequiredService<IAccountService>();
    private readonly IAuthenticationService _authenticationService = service.GetRequiredService<IAuthenticationService>();
    private readonly IToastService _toastService = service.GetRequiredService<IToastService>();
    private readonly IMessenger _messanger = service.GetRequiredService<IMessenger>();
    
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Account Name is required.")]
    [MaxLength(100, ErrorMessage = "Account Name cannot exceed 100 characters.")]
    [MinLength(5, ErrorMessage = "Account Name cannot be less than 5 characters.")]
    [CustomValidation(typeof(AccountCreateViewModel), nameof(ValidateName))]
    private string? _accountName;
    
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Starting Balance is required.")]
    [CustomValidation(typeof(AccountCreateViewModel), nameof(ValidateStartingBalance))]
    private string? _startBalance;
    
    [ObservableProperty]
    private bool _isReadOnly = false;
    
    
    public override Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    [RelayCommand(CanExecute = nameof(CanCreate))]
    private async Task CreateAccount()
    {
        if (!_authenticationService.IsAuthenticated)
        {
            await _toastService.ErrorAsync("No User is Authenticated yet.. Invalid Operation");
            return;
        }
        
        if (!ValidateAll()) return;
        
        if(!decimal.TryParse(StartBalance, out var startBalance))
            startBalance = 100m;
        
        var account = new Account()
        {
            Name = AccountName!,
            Balance = startBalance,
            Id = Guid.NewGuid(),
            AccountHolderId = _authenticationService.CurrentUser!.Id,
        };
        
        var result = await _accountService.CreateAsync(account);
        await _toastService.ShowResultAsync(result,
            "Account Creation",
            "Account Successfully Created");

        if (result.IsValid)
        {
            IsReadOnly = true;
            _messanger.Send(new AccountCreatedMessage(result.Value!));
        }
    }
    
    private bool CanCreate()
    {
        return !IsReadOnly;
    }
    
    public static ValidationResult ValidateName(string name, ValidationContext context)
    {
        if (context.ObjectInstance is not AccountCreateViewModel vm
            || vm._accountService.IsAccountNameUnique(name))
        {
            return ValidationResult.Success!;
        }

        return new ValidationResult("Account name already exists.");
    }
    
    public static ValidationResult ValidateStartingBalance(string balanceText, ValidationContext context)
    {
        if (string.IsNullOrWhiteSpace(balanceText))
            return new ValidationResult("Starting balance is required.");

        if (!decimal.TryParse(balanceText, out var balance))
            return new ValidationResult("Invalid starting balance format.");

        if (balance < 100 || balance > 1000)
            return new ValidationResult("Starting balance must be between 100 and 1000.");

        return ValidationResult.Success!;
    }
}