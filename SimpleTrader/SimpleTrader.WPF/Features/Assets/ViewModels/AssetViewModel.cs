using SimpleTrader.WPF.Features.Home.ViewModels;

namespace SimpleTrader.WPF.Features.Assets.ViewModels;

public class AssetViewModel : ViewModelBase
{
    public string Symbol { get; }
    public int Shares { get; }

    public AssetViewModel(string symbol, int shares)
    {
        Symbol = symbol;
        Shares = shares;
    }
}