using System;
using System.Collections.Generic;
using FieldOps.Kernel.Entities;

namespace SimpleTrader.WPF.Domain.Models;

public class User : EntityBase
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public DateTime DatedJoined { get; set; }

    public virtual ICollection<Account> UserAccounts { get; set; } = [];
}