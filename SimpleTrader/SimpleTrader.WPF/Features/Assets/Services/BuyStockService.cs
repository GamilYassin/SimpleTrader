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

public class BuyStockService(IServiceProvider service)
    : IBuyStockService
{
    private readonly IStockPriceService _stockPriceService = service.GetRequiredService<IStockPriceService>();
    private readonly IAccountService _accountService = service.GetRequiredService<IAccountService>();
    private readonly IRepository<AssetTransaction> _transactionRepo = service.GetRequiredService<IRepository<AssetTransaction>>();
    
    public async Task<Validation<Account>> BuyStockAsync(Account? buyer, Asset asset, int shares)
    {
        var result = GuardAgainstNull(buyer)
            .Bind(_ => GuardAgainstNull(asset))
            .Bind(_ => GuardAgainstZeroOrNegative(shares));
            
        var updatedAsset = await _stockPriceService.UpdateAssetPriceAsync(asset);
        if (updatedAsset.IsFail)
            return Invalid<Account>(updatedAsset.Error!);
        
        var transactionPrice = updatedAsset.Value!.PricePerShare * shares;
        if (transactionPrice > buyer!.Balance)
            return Invalid<Account>($"Buyer Balance {buyer.Balance} can not Support this Purchase operation of price {transactionPrice}");

        var transaction = new AssetTransaction()
        {
            AccountId = buyer.Id,
            PricePerShare = updatedAsset.Value!.PricePerShare,
            Symbol = updatedAsset.Value!.Symbol,
            DateProcessed = DateTime.Now,
            Shares = shares,
            IsPurchase = true
        };

        var createResult = await _transactionRepo.CreateAsync(transaction);
        if (createResult.IsFail)
            return Invalid<Account>(createResult.Error!);
        
        buyer.Balance -= transactionPrice;
        var updateResult = await _accountService.UpdateAsync(buyer.Id, buyer);
        if (updateResult.IsValid)
            return updateResult;

        await _transactionRepo.DeleteAsync(createResult.Value!.Id);
        return updateResult;
    }
}