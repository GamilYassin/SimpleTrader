#region

using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using SimpleTrader.WPF.Features.Accounts.ViewModels;

#endregion

namespace SimpleTrader.WPF.Features.Accounts.Views;

public partial class AccountListView : UserControl
{
    private readonly IServiceProvider _service;
    private AccountListViewModel _viewModel;

    public AccountListView(IServiceProvider service)
    {
        _service = service;
        InitializeComponent();
        _viewModel = new AccountListViewModel(service);
        DataContext = _viewModel;
    }

    private async void AccountListView_OnLoaded(object sender, RoutedEventArgs e)
    {
        await _viewModel.InitializeAsync();
    }

    private async Task OpenAccountDialog()
    {
        var accountView = new AccountCreateView(_service);
        Presenter.Content = accountView;
        await DialogHostLogIn.ShowDialog(Presenter);
    }

    private async void CreateAccountDialog_OnClick(object sender, RoutedEventArgs e)
    {
        await OpenAccountDialog();
    }
}