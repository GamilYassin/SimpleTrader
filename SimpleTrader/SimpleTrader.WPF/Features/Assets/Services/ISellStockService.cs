using System;
using System.Threading.Tasks;
using FieldOps.Kernel.Functional;
using SimpleTrader.WPF.AppServices.Exceptions;
using SimpleTrader.WPF.Features.Accounts.Models;
using SimpleTrader.WPF.Features.Assets.Models;

namespace SimpleTrader.WPF.Features.Assets.Services;

public interface ISellStockService
{
    /// <summary>
    /// Sell a stock for an account.
    /// </summary>
    /// <param name="seller">The account of the seller.</param>
    /// <param name="asset"></param>
    /// <param name="shares">The amount of shares to sell.</param>
    /// <returns>The updated account.</returns>
    /// <exception cref="InsufficientSharesException">Thrown if the seller has insufficient shares for the symbol.</exception>
    /// <exception cref="InvalidSymbolException">Thrown if the purchased symbol is invalid.</exception>
    /// <exception cref="Exception">Thrown if the transaction fails.</exception>
    Task<Validation<Account>> SellStockAsync(Account? seller, Asset asset, int shares);
}