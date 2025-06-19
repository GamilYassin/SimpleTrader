using System;
using System.Threading.Tasks;
using FieldOps.Kernel.Functional;
using SimpleTrader.WPF.AppServices.Exceptions;
using SimpleTrader.WPF.Features.Assets.Models;

namespace SimpleTrader.WPF.Features.Financials.Services;

public interface IStockPriceService
{
    /// <summary>
    /// Get the share price for a symbol.
    /// </summary>
    /// <param name="asset"></param>
    /// <returns>The price of symbol.</returns>
    /// <exception cref="InvalidSymbolException">Thrown if symbol does not exist.</exception>
    /// <exception cref="Exception">Thrown if getting the symbol fails.</exception>
    Task<Validation<Asset>> UpdateAssetPriceAsync(Asset asset);
}