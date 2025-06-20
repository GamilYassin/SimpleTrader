using System;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using SimpleTrader.WPF.Features.Accounts.ViewModels;

namespace SimpleTrader.WPF.Features.Accounts.Views;

public partial class AccountCreateView : UserControl
{
    private AccountCreateViewModel _viewModel;
    public AccountCreateView(IServiceProvider service)
    {
        InitializeComponent();
        _viewModel = new AccountCreateViewModel(service);
        DataContext = _viewModel;
    }

    private async void AccountCreateView_OnLoaded(object sender, RoutedEventArgs e)
    {
        await _viewModel.InitializeAsync();
    }

    private void CloseButton_OnClick(object sender, RoutedEventArgs e)
    {
        DialogHost.CloseDialogCommand.Execute(null, null);
    }
}