using System;
using System.Threading.Tasks;
using SimpleTrader.WPF.Features.Accounts.Models;
using SimpleTrader.WPF.Features.Accounts.Stores;
using SimpleTrader.WPF.Features.Assets.Services;
using SimpleTrader.WPF.Features.Users.DTOs;

namespace SimpleTrader.WPF.Features.Users.Stores;

public class Authenticator : IAuthenticator
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IAccountStore _accountStore;

    public Authenticator(IAuthenticationService authenticationService, IAccountStore accountStore)
    {
        _authenticationService = authenticationService;
        _accountStore = accountStore;
    }

    public Account? CurrentAccount
    {
        get
        {
            return _accountStore.CurrentAccount;
        }
        private set
        {
            _accountStore.CurrentAccount = value;
            StateChanged?.Invoke();
        }
    }

    public bool IsLoggedIn => CurrentAccount != null;

    public event Action StateChanged;

    public async Task Login(string username, string password)
    {
        CurrentAccount = await _authenticationService.LoginAsync(username, password);
    }

    public void Logout()
    {
        CurrentAccount = null;
    }

    public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
    {
        var loginDto = new LoginDto
        (
            UserName : username,
            Email : email,
            Password : password,
            ConfirmedPassword : confirmPassword
        );
        return await _authenticationService.RegisterAsync(loginDto);
    }
}