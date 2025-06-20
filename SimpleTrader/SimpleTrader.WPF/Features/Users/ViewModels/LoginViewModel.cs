using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FieldOps.Kernel.Validators;
using MaterialDesignThemes.Wpf;
using SimpleTrader.WPF.Resources.ViewModels;

namespace SimpleTrader.WPF.Features.Users.ViewModels;

public partial class LoginViewModel(IServiceProvider service) : ViewModelBase(service)
{
    [ObservableProperty]
    private string? _username;


    [RelayCommand]
    private void ExecuteLogin()
    {
        // Validate login
        if (Username.HasValue() && Username!.Equals("admin", StringComparison.CurrentCultureIgnoreCase) ) // replace with real auth
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
    
    public override Task InitializeAsync()
    {
        return Task.CompletedTask;
    }
}