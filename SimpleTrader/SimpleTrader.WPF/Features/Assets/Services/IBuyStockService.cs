using System;
using System.Threading.Tasks;
using SimpleTrader.WPF.AppServices.Exceptions;
using SimpleTrader.WPF.Features.Accounts.Models;

namespace SimpleTrader.WPF.Features.Assets.Services;

public interface IBuyStockService
{
    /// <summary>
    /// Purchase a stock for an account.
    /// </summary>
    /// <param name="buyer">The account of the buyer.</param>
    /// <param name="symbol">The symbol purchased.</param>
    /// <param name="shares">The amount of shares.</param>
    /// <returns>The updated account.</returns>
    /// <exception cref="InsufficientFundsException">Thrown if the acccount has an insufficient balance.</exception>
    /// <exception cref="InvalidSymbolException">Thrown if the purchased symbol is invalid.</exception>
    /// <exception cref="Exception">Thrown if the transaction fails.</exception>
    Task<Account?> BuyStock(Account? buyer, string symbol, int shares);
}