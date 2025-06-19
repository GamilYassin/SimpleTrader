using System;
using System.Windows.Input;
using SimpleTrader.WPF.AppServices.Navigation;
using SimpleTrader.WPF.AppServices.ViewModelFactories;

namespace SimpleTrader.WPF.Resources.Commands;

public class UpdateCurrentViewModelCommand : ICommand
{
    public event EventHandler CanExecuteChanged;

    private readonly INavigator _navigator;
    private readonly ISimpleTraderViewModelFactory _viewModelFactory;

    public UpdateCurrentViewModelCommand(INavigator navigator, ISimpleTraderViewModelFactory viewModelFactory)
    {
        _navigator = navigator;
        _viewModelFactory = viewModelFactory;
    }

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        if(parameter is ViewType)
        {
            var viewType = (ViewType)parameter;

            _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
        }
    }
}