using SimpleTrader.WPF.State.Assets;

namespace SimpleTrader.WPF.ViewModels;

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