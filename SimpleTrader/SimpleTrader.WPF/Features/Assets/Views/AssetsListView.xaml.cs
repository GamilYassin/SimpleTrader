using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore.Metadata;
using SimpleTrader.WPF.Features.Assets.ViewModels;

namespace SimpleTrader.WPF.Features.Assets.Views;

public partial class AssetsListView : UserControl
{
    private AssetsListViewModel _viewModel;
    public AssetsListView(IServiceProvider service)
    {
        InitializeComponent();
        _viewModel = new AssetsListViewModel(service);
        DataContext = _viewModel;
    }

    private async void AssetsListView_OnLoaded(object sender, RoutedEventArgs e)
    {
        await _viewModel.InitializeAsync();
    }
}