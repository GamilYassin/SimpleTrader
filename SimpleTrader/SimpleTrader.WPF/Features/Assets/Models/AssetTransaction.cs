using System;
using FieldOps.Kernel.Entities;
using SimpleTrader.WPF.Features.Accounts.Models;

namespace SimpleTrader.WPF.Features.Assets.Models;

public class AssetTransaction : EntityBase
{
    public bool IsPurchase { get; set; }
    public int Shares { get; set; }
    public DateTime DateProcessed { get; set; }
    
    // relations
    public Guid AccountId { get; set; }
    public virtual Account? Account { get; set; }
    public Asset Asset { get; set; }
}