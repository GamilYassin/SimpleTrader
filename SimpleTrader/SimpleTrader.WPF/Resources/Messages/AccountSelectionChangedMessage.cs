using CommunityToolkit.Mvvm.Messaging.Messages;
using SimpleTrader.WPF.Features.Accounts.Models;

namespace SimpleTrader.WPF.Resources.Messages;

public class AccountSelectionChangedMessage(Account account) : ValueChangedMessage<Account>(account);