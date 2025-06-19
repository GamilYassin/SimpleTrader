using System.Windows.Input;
using SimpleTrader.WPF.AppServices.Navigation;
using SimpleTrader.WPF.AppServices.ViewModelFactories;
using SimpleTrader.WPF.Features.Users.Stores;
using SimpleTrader.WPF.Resources.Commands;

namespace SimpleTrader.WPF.Features.Home.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly ISimpleTraderViewModelFactory _viewModelFactory;
    private readonly INavigator _navigator;
    private readonly IAuthenticator _authenticator;

    public bool IsLoggedIn => _authenticator.IsLoggedIn;
    public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;

    public ICommand UpdateCurrentViewModelCommand { get; }

    public MainViewModel(INavigator navigator, ISimpleTraderViewModelFactory viewModelFactory, IAuthenticator authenticator)
    {
        _navigator = navigator;
        _viewModelFactory = viewModelFactory;
        _authenticator = authenticator;

        _navigator.StateChanged += Navigator_StateChanged;
        _authenticator.StateChanged += Authenticator_StateChanged;

        UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, _viewModelFactory);
        UpdateCurrentViewModelCommand.Execute(ViewType.Login);
    }

    private void Authenticator_StateChanged()
    {
        OnPropertyChanged(nameof(IsLoggedIn));
    }

    private void Navigator_StateChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }

    public override void Dispose()
    {
        _navigator.StateChanged -= Navigator_StateChanged;
        _authenticator.StateChanged -= Authenticator_StateChanged;

        base.Dispose();
    }
}