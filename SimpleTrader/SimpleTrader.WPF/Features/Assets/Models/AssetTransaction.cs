using System;
using FieldOps.Kernel.Entities;
using SimpleTrader.WPF.Features.Accounts.Models;

namespace SimpleTrader.WPF.Features.Assets.Models;

public class AssetTransaction : EntityBase
{
    public required string Symbol { get; set; }
    public decimal PricePerShare { get; set; }
    public bool IsPurchase { get; set; }
    public int Shares { get; set; }
    public DateTime DateProcessed { get; set; }
    
    // relations
    public Guid AccountId { get; set; }
    public virtual Account? Account { get; set; }
}