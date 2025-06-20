#region

using System;
using System.Windows.Controls;
using SimpleTrader.WPF.Features.Users.ViewModels;

#endregion

namespace SimpleTrader.WPF.Features.Users.Views;

public partial class LoginView : UserControl
{
    private readonly LoginViewModel _viewModel;
    public LoginView(IServiceProvider service)
    {
        InitializeComponent();
        _viewModel = new LoginViewModel(service);
        DataContext = _viewModel;
    }
}