using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using MaterialDesignThemes.Wpf;
using SimpleTrader.WPF.Features.Users.Views;
using SimpleTrader.WPF.Resources.ViewModels;

namespace SimpleTrader.WPF.Features.Home.ViewModels;

public class MainWindowViewModel(IServiceProvider service) : ViewModelBase(service)
{
    public override Task InitializeAsync()
    {
       return Task.CompletedTask;
    }
    

}