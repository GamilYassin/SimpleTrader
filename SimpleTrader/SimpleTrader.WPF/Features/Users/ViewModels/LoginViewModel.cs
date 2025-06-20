using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using SimpleTrader.WPF.AppServices.Toast;
using SimpleTrader.WPF.Features.Accounts.Services;
using SimpleTrader.WPF.Features.Users.Services;
using SimpleTrader.WPF.Resources.ViewModels;

namespace SimpleTrader.WPF.Features.Users.ViewModels;

public partial class LoginViewModel(IServiceProvider service) : ViewModelBase(service)
{
    private readonly IAuthenticationService  _authenticationService = service.GetRequiredService<IAuthenticationService>();
    private readonly IAccountService _accountService = service.GetRequiredService<IAccountService>();
    private readonly IToastService  _toastService = service.GetRequiredService<IToastService>();
    
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "User name is required.")]
    [MaxLength(100, ErrorMessage = "User name cannot exceed 100 characters.")]
    [MinLength(5, ErrorMessage = "User name cannot be less than 5 characters.")]
    private string? _userName;
    
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Password is required.")]
    [MaxLength(8, ErrorMessage = "User name cannot exceed 8 characters.")]
    [MinLength(3, ErrorMessage = "User name cannot be less than 3 characters.")]
    private string? _password;

    [RelayCommand]
    private async void ExecuteLogin()
    {
        if (!ValidateAll()) return;
        // Validate login
        var result = await _authenticationService.LoginAsync(UserName!, Password!);
        if (result.IsValid)
        {
            await _toastService.SuccessAsync("User logged in.");
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        else
        {
            await _toastService.ErrorAsync("LogIn Failed. Either Username or password is incorrect");
        }
    }
    
    [RelayCommand]
    private async void RegisterUser()
    {
        await _toastService.InfoAsync("New User Registration is not Implemented yet!");
    }
    
    public override Task InitializeAsync()
    {
        return Task.CompletedTask;
    }
    
    private bool ValidateAll()
    {
        ValidateAllProperties();

        if (HasErrors)
        {
            return false;
        }

        return !HasErrors;
    }
}