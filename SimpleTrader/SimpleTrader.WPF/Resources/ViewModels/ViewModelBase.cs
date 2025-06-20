using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SimpleTrader.WPF.Resources.ViewModels;

public abstract partial class ViewModelBase(IServiceProvider service) : ObservableValidator
{
    public abstract Task InitializeAsync();
}