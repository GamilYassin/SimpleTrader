using System;
using System.Threading.Tasks;
using FieldOps.Kernel.Functional;
using Microsoft.Extensions.DependencyInjection;
using SimpleTrader.WPF.Data.Repositories;
using SimpleTrader.WPF.Features.Accounts.Models;
using SimpleTrader.WPF.Features.Accounts.Services;
using SimpleTrader.WPF.Features.Assets.Models;
using SimpleTrader.WPF.Features.Financials.Services;

namespace SimpleTrader.WPF.Features.Assets.Services;

public class SellStockService(IServiceProvider service) : ISellStockService
{
    private readonly IStockPriceService _stockPriceService = service.GetRequiredService<IStockPriceService>();
    private readonly IAccountService _accountService = service.GetRequiredService<IAccountService>();
    private readonly IRepository<AssetTransaction> _transactionRepo = service.GetRequiredService<IRepository<AssetTransaction>>();

    public async Task<Validation<Account>> SellStockAsync(Account? seller, Asset asset, int shares)
    {
        var getResult = GuardAgainstNull(seller)
            .Bind(_ => GuardAgainstNull(asset))
            .Bind(_ => GuardAgainstZeroOrNegative(shares));

        if (getResult.IsFail)
            return Invalid<Account>(getResult.Error!);
        
        
        // Validate seller has sufficient shares.
        var accountShares = await _accountService.GetSharesCountAsync(seller!, asset);
        if(accountShares < shares)
            return Invalid<Account>($"Account has shares count {accountShares} which are less than Sell request of {shares} shares!");

        var stockPrice = await _stockPriceService.UpdateAssetPriceAsync(asset);
        if(stockPrice.IsFail)
            return Invalid<Account>(stockPrice.Error!);
        
        var transaction = new AssetTransaction()
        {
            AccountId = seller!.Id,
            PricePerShare = stockPrice.Value!.PricePerShare,
            Symbol = stockPrice.Value!.Symbol,
            DateProcessed = DateTime.Now,
            IsPurchase = false,
            Shares = shares
        };

        var createResult = await _transactionRepo.CreateAsync(transaction);
        if (createResult.IsFail)
            return Invalid<Account>(createResult.Error!);
        
        seller.Balance += stockPrice.Value.PricePerShare * shares;
        
        var updateResult = await _accountService.UpdateAsync(seller.Id, seller);
        if (updateResult.IsValid)
            return updateResult;

        await _transactionRepo.DeleteAsync(updateResult.Value!.Id);
        return updateResult;
    }
}