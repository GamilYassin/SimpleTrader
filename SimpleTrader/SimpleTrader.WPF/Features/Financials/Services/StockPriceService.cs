using System;
using System.Threading.Tasks;
using FieldOps.Kernel.Functional;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using SimpleTrader.WPF.AppServices.Exceptions;
using SimpleTrader.WPF.Data.Repositories;
using SimpleTrader.WPF.Features.Assets.DataSeed;
using SimpleTrader.WPF.Features.Assets.Models;
using SimpleTrader.WPF.Features.Financials.DTOs;
using SimpleTrader.WPF.Features.Financials.Models;

namespace SimpleTrader.WPF.Features.Financials.Services;

public class StockPriceService(IServiceProvider service) : IStockPriceService
{
    public async Task<Validation<Asset>> UpdateAssetPriceAsync(Asset asset)
    {
        // TODO: Refactor this method to get price from database or from API based on customized timer
        AssetPriceSimulator.UpdatePrice(asset);
        var repo = service.GetRequiredService<IRepository<Asset>>();
        return await repo.UpdateAsync(asset.Id, asset);


        // var uri = "stock/real-time-price/" + asset.Symbol;
        //
        // var stockPriceResult = await client.GetAsync<StockPriceResult>(uri);
        //
        // if(stockPriceResult.Price == 0)
        // {
        //     throw new InvalidSymbolException(asset.Symbol);
        // }
        //
        // return stockPriceResult.Price;
    }
}