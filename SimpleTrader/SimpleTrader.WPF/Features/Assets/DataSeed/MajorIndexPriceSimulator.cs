using System;
using SimpleTrader.WPF.Features.Assets.Models;

namespace SimpleTrader.WPF.Features.Assets.DataSeed;

public static class MajorIndexPriceSimulator
{
    private static readonly Random Random = new();

    public static void UpdatePrice(MajorIndex index, decimal maxFluctuationPercent = 7m)
    {
        if (index.Price <= 0)
            index.Price = 50m; // fallback for uninitialized assets
        
        // Generate random percentage between -maxFluctuation% and +maxFluctuation%
        var percentChange = (decimal)(Random.NextDouble() * (double)(maxFluctuationPercent * 2)) - maxFluctuationPercent;

        // Apply the fluctuation
        index.Changes = index.Price * percentChange / 100m;
        index.Price = Math.Round(index.Price + index.Changes, 2);
    }
}
