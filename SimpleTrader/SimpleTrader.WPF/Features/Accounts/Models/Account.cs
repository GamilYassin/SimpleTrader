using System;
using System.Collections.Generic;
using FieldOps.Kernel.Entities;
using SimpleTrader.WPF.Features.Assets.Models;
using SimpleTrader.WPF.Features.Users.Models;

namespace SimpleTrader.WPF.Features.Accounts.Models;

public class Account : EntityBase
{
    public required string Name { get; set; }
    public decimal Balance { get; set; }
    public Guid AccountHolderId { get; set; }
    public virtual User? AccountHolder { get; set; }
    public virtual ICollection<AssetTransaction> AssetTransactions { get; set; } = [];
}