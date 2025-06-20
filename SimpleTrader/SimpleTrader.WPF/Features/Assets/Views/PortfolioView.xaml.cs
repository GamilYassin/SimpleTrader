#region

using System;
using System.Windows;
using System.Windows.Controls;
using SimpleTrader.WPF.Features.Assets.ViewModels;

#endregion

namespace SimpleTrader.WPF.Features.Assets.Views;

/// <summary>
///     Interaction logic for PortfolioView.xaml
/// </summary>
public partial class PortfolioView : UserControl
{
    private PortfolioViewModel _viewModel;

    public PortfolioView(IServiceProvider service)
    {
        InitializeComponent();
        _viewModel = new PortfolioViewModel(service);
        DataContext = _viewModel;
    }

    private async void PortfolioView_OnLoaded(object sender, RoutedEventArgs e)
    {
        await _viewModel.InitializeAsync();
    }
}