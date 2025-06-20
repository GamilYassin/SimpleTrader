using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using MaterialDesignThemes.Wpf;
using SimpleTrader.WPF.Features.Users.Views;
using SimpleTrader.WPF.Resources.Messages;
using SimpleTrader.WPF.Resources.ViewModels;

namespace SimpleTrader.WPF.Features.Home.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, IRecipient<LoggedInUserChangedMessage>
{
    [ObservableProperty]
    private string? _userName =  string.Empty;
    
    public MainWindowViewModel(IServiceProvider service) : base(service)
    {
        Messenger.RegisterAll(this);
    }

    public override Task InitializeAsync()
    {
       return Task.CompletedTask;
    }

    public void Receive(LoggedInUserChangedMessage message)
    {
        UserName = message.Value.Username;
    }
}