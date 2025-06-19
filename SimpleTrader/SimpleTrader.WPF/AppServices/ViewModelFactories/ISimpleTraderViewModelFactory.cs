using SimpleTrader.WPF.AppServices.Navigation;
using SimpleTrader.WPF.Features.Home.ViewModels;

namespace SimpleTrader.WPF.AppServices.ViewModelFactories;

public interface ISimpleTraderViewModelFactory
{
    ViewModelBase CreateViewModel(ViewType viewType);
}