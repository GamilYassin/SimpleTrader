using System;
using SimpleTrader.WPF.Features.Home.ViewModels;

namespace SimpleTrader.WPF.AppServices.Navigation;

public enum ViewType
{
    Login,
    Home,
    Portfolio,
    Buy,
    Sell
}

public interface INavigator
{
    ViewModelBase CurrentViewModel { get; set; }
    event Action StateChanged;
}