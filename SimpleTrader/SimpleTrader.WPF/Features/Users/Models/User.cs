using System;
using System.Collections.Generic;
using FieldOps.Kernel.Entities;
using SimpleTrader.WPF.Features.Accounts.Models;

namespace SimpleTrader.WPF.Features.Users.Models;

public class User : EntityBase
{
    public required string Email { get; set; }
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public DateTime DatedJoined { get; set; }
    public virtual ICollection<Account> UserAccounts { get; set; } = [];
}