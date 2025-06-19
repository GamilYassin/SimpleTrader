using System;
using System.Collections.Generic;
using System.Text;
using SimpleTrader.WPF.Domain.Models;

namespace SimpleTrader.WPF.State.Accounts;

public interface IAccountStore
{
    Account CurrentAccount { get; set; }
    event Action StateChanged;
}