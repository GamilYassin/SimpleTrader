using System;
using FieldOps.Kernel.Entities;

namespace SimpleTrader.WPF.Domain.Models;

public class AssetTransaction : EntityBase
{
    public bool IsPurchase { get; set; }
    public int Shares { get; set; }
    public DateTime DateProcessed { get; set; }
    
    // relations
    public Guid AccountId { get; set; }
    public Account Account { get; set; }
    public Asset Asset { get; set; }
}