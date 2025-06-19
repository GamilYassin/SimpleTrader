using System;
using System.Threading.Tasks;
using SimpleTrader.WPF.Domain.Exceptions;
using SimpleTrader.WPF.Domain.Models;

namespace SimpleTrader.WPF.Domain.Services.Abstractions;

public interface ISellStockService
{
    /// <summary>
    /// Sell a stock for an account.
    /// </summary>
    /// <param name="seller">The account of the seller.</param>
    /// <param name="symbol">The symbol sold.</param>
    /// <param name="shares">The amount of shares to sell.</param>
    /// <returns>The updated account.</returns>
    /// <exception cref="InsufficientSharesException">Thrown if the seller has insufficient shares for the symbol.</exception>
    /// <exception cref="InvalidSymbolException">Thrown if the purchased symbol is invalid.</exception>
    /// <exception cref="Exception">Thrown if the transaction fails.</exception>
    Task<Account?> SellStock(Account? seller, string symbol, int shares);
}