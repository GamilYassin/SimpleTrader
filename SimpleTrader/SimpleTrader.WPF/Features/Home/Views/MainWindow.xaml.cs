#region

using System;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SimpleTrader.WPF.Features.Accounts.Views;
using SimpleTrader.WPF.Features.Assets.Views;
using SimpleTrader.WPF.Features.Home.ViewModels;
using SimpleTrader.WPF.Features.Users.Views;

#endregion

namespace SimpleTrader.WPF.Features.Home.Views;

public partial class MainWindow : Window
{
    private readonly IServiceProvider _service;
    private readonly MainWindowViewModel _viewModel;
    public MainWindow(IServiceProvider service)
    {
        _service = service;
        InitializeComponent();
        _viewModel = new MainWindowViewModel(service);
        DataContext = _viewModel;
    }

    private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        await _viewModel.InitializeAsync();
        await ShowLoginOverlay();
        
        // AccountList
        var accountListView = new AccountListView(_service);
        AccountListPresenter.Content = accountListView;
        
        // Portfolio
        var portfolioView = new PortfolioView(_service);
        PortfolioPresenter.Content = portfolioView;
        
        // Buy Asset
        var buyView = new BuyView(_service);
        BuyPresenter.Content = buyView;
    }
    
    private async Task ShowLoginOverlay()
    {
        var loginView = new LoginView(_service);
        Presenter.Content = loginView;
        await DialogHostLogIn.ShowDialog(Presenter);
    }
}