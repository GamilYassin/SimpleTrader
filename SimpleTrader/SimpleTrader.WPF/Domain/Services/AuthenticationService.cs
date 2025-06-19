using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SimpleTrader.WPF.Domain.DTOs;
using SimpleTrader.WPF.Domain.Exceptions;
using SimpleTrader.WPF.Domain.Models;
using SimpleTrader.WPF.Domain.Services.Abstractions;
using Throw;

namespace SimpleTrader.WPF.Domain.Services;

public class AuthenticationService(IAccountService accountService, IPasswordHasher passwordHasher)
    : IAuthenticationService
{
    public async Task<Account?> LoginAsync(string username, string password)
    {
        var storedAccount = await accountService.GetByUserNameAsync(username);
        storedAccount.ThrowIfNull(() => new UserNotFoundException(username));
        var passwordResult = passwordHasher.VerifyHashedPassword(storedAccount.AccountHolder.PasswordHash, password);
        passwordResult.Throw()
            .IfNotEquals(PasswordVerificationResult.Success);
        return storedAccount;
    }

    public async Task<RegistrationResult> RegisterAsync(LoginDto loginDto)
    {
        if(loginDto.Password != loginDto.ConfirmedPassword)
        {
            return RegistrationResult.PasswordsDoNotMatch;
        }

        if(await accountService.IsEmailExistsAsync(loginDto.Email))
        {
            return RegistrationResult.EmailAlreadyExists;
        }

        if (await accountService.IsUserNameExistsAsync(loginDto.UserName))
        {
            return RegistrationResult.UsernameAlreadyExists;
        }

        var hashedPassword = passwordHasher.HashPassword(loginDto.Password);

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

        await accountService.CreateAsync(account);
        return RegistrationResult.Success;
    }
}