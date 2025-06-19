using System;
using System.Threading.Tasks;
using FieldOps.Kernel.Functional;
using FieldOps.Kernel.PasswordService;
using Microsoft.Extensions.DependencyInjection;
// using Microsoft.AspNet.Identity;
using SimpleTrader.WPF.Features.Accounts.Models;
using SimpleTrader.WPF.Features.Accounts.Services;
using SimpleTrader.WPF.Features.Users.DTOs;
using SimpleTrader.WPF.Features.Users.Enums;
using SimpleTrader.WPF.Features.Users.Models;
using Throw;

namespace SimpleTrader.WPF.Features.Users.Services;

public class AuthenticationService(IServiceProvider service) : IAuthenticationService
{
    private readonly IAccountService _accountService = service.GetRequiredService<IAccountService>();
    private readonly IPasswordHasher _passwordHasher = service.GetRequiredService<IPasswordHasher>();
    
    public async Task<Validation<Account>> LoginAsync(string username, string password)
    {
        var storedAccount = await _accountService.GetByUserNameAsync(username);
        if (storedAccount.IsFail)
            return storedAccount;
        
        var passwordResult = _passwordHasher.VerifyHashedPassword( storedAccount.Value!.AccountHolder!.PasswordHash, password);
        passwordResult.Throw()
            .IfNotEquals(PasswordVerificationResult.Success);
        return passwordResult ==  PasswordVerificationResult.Success
            ? storedAccount
            : Invalid<Account>("Either Username or password is incorrect");
    }

    public async Task<RegistrationResult> RegisterAsync(LoginDto loginDto)
    {
        if(loginDto.Password != loginDto.ConfirmedPassword)
        {
            return RegistrationResult.PasswordsDoNotMatch;
        }

        if(await _accountService.IsEmailExistsAsync(loginDto.Email))
        {
            return RegistrationResult.EmailAlreadyExists;
        }

        if (await _accountService.IsUserNameExistsAsync(loginDto.UserName))
        {
            return RegistrationResult.UsernameAlreadyExists;
        }

        var hashedPassword = _passwordHasher.HashPassword(loginDto.Password);

        var user = new User()
        {
            Email = loginDto.Email,
            Username = loginDto.UserName,
            PasswordHash = hashedPassword,
            DatedJoined = DateTime.Now
        };

        var account = new Account()
        {
            AccountHolder = user,
            Balance = 500
        };

        await _accountService.CreateAsync(account);
        return RegistrationResult.Success;
    }
}