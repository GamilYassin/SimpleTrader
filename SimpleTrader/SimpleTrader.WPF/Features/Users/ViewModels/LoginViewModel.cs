using System.Windows.Input;
using SimpleTrader.WPF.AppServices.Navigation;
using SimpleTrader.WPF.Features.Home.ViewModels;
using SimpleTrader.WPF.Features.Users.Stores;
using SimpleTrader.WPF.Resources.Commands;

namespace SimpleTrader.WPF.Features.Users.ViewModels;

public class LoginViewModel : ViewModelBase
{
	private string _username = "SingletonSean";
	public string Username
	{
		get
		{
			return _username;
		}
		set
		{
			_username = value;
			OnPropertyChanged(nameof(Username));
			OnPropertyChanged(nameof(CanLogin));
		}
	}

	private string _password;
	public string Password
	{
		get
		{
			return _password;
		}
		set
		{
			_password = value;
			OnPropertyChanged(nameof(Password));
			OnPropertyChanged(nameof(CanLogin));
		}
	}

	public bool CanLogin => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);

	public MessageViewModel ErrorMessageViewModel { get; }

	public string ErrorMessage
	{
		set => ErrorMessageViewModel.Message = value;
	}

	public ICommand LoginCommand { get; }
	public ICommand ViewRegisterCommand { get; }

	public LoginViewModel(IAuthenticator authenticator, IRenavigator loginRenavigator, IRenavigator registerRenavigator)
	{
		ErrorMessageViewModel = new MessageViewModel();

		LoginCommand = new LoginCommand(this, authenticator, loginRenavigator);
		ViewRegisterCommand = new RenavigateCommand(registerRenavigator);
	}

	public override void Dispose()
	{
		ErrorMessageViewModel.Dispose();

		base.Dispose();
	}
}