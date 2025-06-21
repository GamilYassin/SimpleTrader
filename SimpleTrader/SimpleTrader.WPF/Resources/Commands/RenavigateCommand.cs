using System;
using System.Windows.Input;
using SimpleTrader.WPF.AppServices.Navigation;

namespace SimpleTrader.WPF.Resources.Commands;

public class RenavigateCommand(IRenavigator renavigator) : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        renavigator.Renavigate();
    }
}