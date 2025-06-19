using System.ComponentModel.DataAnnotations;

namespace SimpleTrader.WPF.Features.Assets.Models;

public class Asset
{
    [MaxLength(5)]
    public required string Symbol { get; set; }
    public double PricePerShare { get; set; }
}