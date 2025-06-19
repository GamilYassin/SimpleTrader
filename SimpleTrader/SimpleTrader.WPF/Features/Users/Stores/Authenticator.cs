using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SimpleTrader.WPF.Features.Accounts.Models;
using SimpleTrader.WPF.Features.Accounts.Stores;
using SimpleTrader.WPF.Features.Users.DTOs;
using SimpleTrader.WPF.Features.Users.Enums;
using SimpleTrader.WPF.Features.Users.Services;

namespace SimpleTrader.WPF.Features.Users.Stores;

public class Authenticator(IServiceProvider service) : IAuthenticator
{
    private readonly IAuthenticationService _authenticationService = service.GetRequiredService<IAuthenticationService>();
    private readonly IAccountStore _accountStore =  service.GetRequiredService<IAccountStore>();

    public Account? CurrentAccount
    {
        get => _accountStore.CurrentAccount;
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
        var result = await _authenticationService.LoginAsync(username, password);
        CurrentAccount = result.IsValid 
            ? result.Value 
            : null;
    }

    public void Logout()
    {
        CurrentAccount = null;
    }

    public async Task<RegistrationResult> Register(LoginDto loginDto)
    {
        return await _authenticationService.RegisterAsync(loginDto);
    }
}