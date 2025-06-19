using SimpleTrader.WPF.State.Accounts;
using System;
using System.Threading.Tasks;
using SimpleTrader.WPF.Domain.Models;
using SimpleTrader.WPF.Domain.Services.Abstractions;

namespace SimpleTrader.WPF.State.Authenticators;

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
        return await _authenticationService.RegisterAsync(email, username, password, confirmPassword);
    }
}