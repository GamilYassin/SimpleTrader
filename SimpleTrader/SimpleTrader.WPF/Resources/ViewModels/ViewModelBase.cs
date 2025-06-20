using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using SimpleTrader.WPF.AppServices.Toast;

namespace SimpleTrader.WPF.Resources.ViewModels;

public abstract partial class ViewModelBase(IServiceProvider service) : ObservableValidator
{
    protected IMessenger  Messenger = service.GetRequiredService<IMessenger>();
    protected IToastService ToastService = service.GetRequiredService<IToastService>();
    
    public abstract Task InitializeAsync();
    
    protected bool ValidateAll()
    {
        ValidateAllProperties();

        if (HasErrors)
        {
            return false;
        }

        return !HasErrors;
    }
}