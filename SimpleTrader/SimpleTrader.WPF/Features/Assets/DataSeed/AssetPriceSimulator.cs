using System;
using SimpleTrader.WPF.Features.Assets.Models;

namespace SimpleTrader.WPF.Features.Assets.DataSeed;

public static class AssetPriceSimulator
{
    private static readonly Random Random = new();

    public static void UpdatePrice(Asset asset, decimal maxFluctuationPercent = 5m)
    {
        if (asset.PricePerShare <= 0)
            asset.PricePerShare = 50m; // fallback for uninitialized assets

        // Generate random percentage between -maxFluctuation% and +maxFluctuation%
        var percentChange = (decimal)(Random.NextDouble() * (double)(maxFluctuationPercent * 2)) - maxFluctuationPercent;

        // Apply the fluctuation
        var changeAmount = asset.PricePerShare * percentChange / 100m;
        asset.PricePerShare = Math.Round(asset.PricePerShare + changeAmount, 2);
    }
}
