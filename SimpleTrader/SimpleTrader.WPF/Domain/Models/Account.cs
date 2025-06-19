using System;
using System.Collections.Generic;
using FieldOps.Kernel.Entities;

namespace SimpleTrader.WPF.Domain.Models;

public class Account : EntityBase
{
    public double Balance { get; set; }
    public Guid AccountHolderId { get; set; }
    public User AccountHolder { get; set; }
    public ICollection<AssetTransaction> AssetTransactions { get; set; }
}