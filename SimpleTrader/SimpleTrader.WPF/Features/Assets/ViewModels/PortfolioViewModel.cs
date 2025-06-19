using SimpleTrader.WPF.Features.Assets.Stores;
using SimpleTrader.WPF.Features.Home.ViewModels;

namespace SimpleTrader.WPF.Features.Assets.ViewModels;

public class PortfolioViewModel : ViewModelBase
{
    public AssetListingViewModel AssetListingViewModel { get; }

    public PortfolioViewModel(AssetStore assetStore)
    {
        AssetListingViewModel = new AssetListingViewModel(assetStore);
    }

    public override void Dispose()
    {
        AssetListingViewModel.Dispose();

        base.Dispose();
    }
}