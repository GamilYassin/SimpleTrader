using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using SimpleTrader.WPF.Resources.ViewModels;

namespace SimpleTrader.WPF.Features.Home.ViewModels;

public partial class HomeViewModel(IServiceProvider service) : ViewModelBase(service)
{
    [ObservableProperty]
    private string? _balance;
    
    
    public async override Task InitializeAsync()
    {
        throw new NotImplementedException();
    }
}