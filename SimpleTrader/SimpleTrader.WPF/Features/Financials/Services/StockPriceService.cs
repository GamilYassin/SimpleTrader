using System.Threading.Tasks;
using SimpleTrader.WPF.AppServices.Exceptions;
using SimpleTrader.WPF.Features.Financials.DTOs;
using SimpleTrader.WPF.Features.Financials.Models;

namespace SimpleTrader.WPF.Features.Financials.Services;

public class StockPriceService : IStockPriceService
{
    private readonly FinancialModelingPrepHttpClient _client;

    public StockPriceService(FinancialModelingPrepHttpClient client)
    {
        _client = client;
    }

    public async Task<double> GetPrice(string symbol)
    {
        // TODO: Refactor this method to get price from database or from API based on customized timer 
        var uri = "stock/real-time-price/" + symbol;

        var stockPriceResult = await _client.GetAsync<StockPriceResult>(uri);

        if(stockPriceResult.Price == 0)
        {
            throw new InvalidSymbolException(symbol);
        }

        return stockPriceResult.Price;
    }
}