#region

using System;
using System.Windows;
using System.Windows.Controls;
using SimpleTrader.WPF.Features.Assets.ViewModels;

#endregion

namespace SimpleTrader.WPF.Features.Assets.Views;

/// <summary>
///     Interaction logic for BuyView.xaml
/// </summary>
public partial class BuyView : UserControl
{
    BuyViewModel _viewModel;
    
    public BuyView(IServiceProvider service)
    {
        InitializeComponent();
        _viewModel = new BuyViewModel(service);
        DataContext = _viewModel;
    }

    private async void BuyView_OnLoaded(object sender, RoutedEventArgs e)
    {
        await _viewModel.InitializeAsync();
    }
}