using System;
using System.Windows.Controls;
using SimpleTrader.WPF.Features.Home.ViewModels;

namespace SimpleTrader.WPF.Features.Home.Views;

/// <summary>
/// Interaction logic for HomeView.xaml
/// </summary>
public partial class HomeView : UserControl
{
    private readonly HomeViewModel _viewModel;
    public HomeView(IServiceProvider service)
    {
        InitializeComponent();
        _viewModel = new HomeViewModel(service);
        DataContext = _viewModel;
    }
}