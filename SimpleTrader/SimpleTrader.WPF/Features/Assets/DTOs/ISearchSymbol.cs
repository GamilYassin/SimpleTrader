namespace SimpleTrader.WPF.Features.Assets.DTOs;

public interface ISearchSymbol
{
    string ErrorMessage { set; }
    string SearchResultSymbol { set; }
    double StockPrice { set; }
    string Symbol { get; }
    bool CanSearchSymbol { get; }
}