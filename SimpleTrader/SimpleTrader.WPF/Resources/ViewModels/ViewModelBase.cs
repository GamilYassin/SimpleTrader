using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleTrader.WPF.Resources.ViewModels;

public abstract partial class ViewModelBase(IServiceProvider service) : ObservableValidator
{
    protected IMessenger  Messenger = service.GetRequiredService<IMessenger>();
    public abstract Task InitializeAsync();
}