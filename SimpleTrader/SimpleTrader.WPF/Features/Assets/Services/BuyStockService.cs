using System;
using System.Threading.Tasks;
using SimpleTrader.WPF.AppServices.Exceptions;
using SimpleTrader.WPF.Data.Repositories;
using SimpleTrader.WPF.Features.Accounts.Models;
using SimpleTrader.WPF.Features.Accounts.Services;
using SimpleTrader.WPF.Features.Assets.Models;
using SimpleTrader.WPF.Features.Financials.Services;

namespace SimpleTrader.WPF.Features.Assets.Services;

public class BuyStockService(IStockPriceService stockPriceService, IAccountService accountService)
    : IBuyStockService
{
    public async Task<Account?> BuyStock(Account? buyer, string symbol, int shares)
    {
        var stockPrice = await stockPriceService.GetPrice(symbol);

        var transactionPrice = stockPrice * shares;

        if (transactionPrice > buyer.Balance)
        {
            throw new InsufficientFundsException(buyer.Balance, transactionPrice);
        }

        var transaction = new AssetTransaction()
        {
            Account = buyer,
            Asset = new Asset()
            {
                PricePerShare = stockPrice,
                Symbol = symbol
            },
            DateProcessed = DateTime.Now,
            Shares = shares,
            IsPurchase = true
        };

        buyer.AssetTransactions.Add(transaction);
        buyer.Balance -= transactionPrice;

        await accountService.UpdateAsync(buyer.Id, buyer);

        return buyer;
    }
}