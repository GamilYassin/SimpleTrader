using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using SimpleTrader.WPF.Data.Repositories;
using SimpleTrader.WPF.Features.Assets.Models;
using SimpleTrader.WPF.Resources.ViewModels;

namespace SimpleTrader.WPF.Features.Assets.ViewModels;

public partial class AssetsListViewModel(IServiceProvider service) : ViewModelBase(service)
{
    private readonly IRepository<Asset> _assetsRepository = service.GetRequiredService<IRepository<Asset>>();
    
    [ObservableProperty]
    private ObservableCollection<Asset> _assets=[];
    
    public override async Task InitializeAsync()
    {
        var assets = await _assetsRepository.GetAllAsync();
        Assets = new ObservableCollection<Asset>(assets);
    }
}