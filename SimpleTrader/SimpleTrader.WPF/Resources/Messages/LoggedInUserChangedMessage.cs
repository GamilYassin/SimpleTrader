using CommunityToolkit.Mvvm.Messaging.Messages;
using SimpleTrader.WPF.Features.Users.Models;

namespace SimpleTrader.WPF.Resources.Messages;

public class LoggedInUserChangedMessage(User user) : ValueChangedMessage<User>(user);