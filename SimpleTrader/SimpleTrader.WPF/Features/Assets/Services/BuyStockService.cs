using System;
using System.Threading.Tasks;
using SimpleTrader.WPF.AppServices.Exceptions;
using SimpleTrader.WPF.Data.Repositories;
using SimpleTrader.WPF.Features.Accounts.Models;
using SimpleTrader.WPF.Features.Assets.Models;
using SimpleTrader.WPF.Features.Financials.Services;

namespace SimpleTrader.WPF.Features.Assets.Services;

public class BuyStockService : IBuyStockService
{
    private readonly IStockPriceService _stockPriceService;
    private readonly IRepository<Account?> _accountService;

    public BuyStockService(IStockPriceService stockPriceService, IRepository<Account?> accountService)
    {
        _stockPriceService = stockPriceService;
        _accountService = accountService;
    }

    public async Task<Account?> BuyStock(Account? buyer, string symbol, int shares)
    {
        double stockPrice = await _stockPriceService.GetPrice(symbol);

        double transactionPrice = stockPrice * shares;

        if (transactionPrice > buyer.Balance)
        {
            throw new InsufficientFundsException(buyer.Balance, transactionPrice);
        }

        AssetTransaction transaction = new AssetTransaction()
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

        await _accountService.UpdateAsync(buyer.Id, buyer);

        return buyer;
    }
}