using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using FieldOps.Kernel.Functional;
using FieldOps.Kernel.PasswordService;
using Microsoft.Extensions.DependencyInjection;
// using Microsoft.AspNet.Identity;
using SimpleTrader.WPF.Features.Users.DTOs;
using SimpleTrader.WPF.Features.Users.Enums;
using SimpleTrader.WPF.Features.Users.Models;
using SimpleTrader.WPF.Resources.Messages;

namespace SimpleTrader.WPF.Features.Users.Services;

public class AuthenticationService(IServiceProvider service) : IAuthenticationService
{
    private readonly IUserService _userService = service.GetRequiredService<IUserService>();
    private readonly IPasswordHasher _passwordHasher = service.GetRequiredService<IPasswordHasher>();
    private readonly IMessenger _messenger = service.GetRequiredService<IMessenger>();

    public async Task<Validation<User>> LoginAsync(string username, string password)
    {
        var userResult = await _userService.GetByUserNameAsync(username);
        if (userResult.IsFail)
            return userResult;
        
        var passwordResult = _passwordHasher.VerifyHashedPassword(userResult.Value!.PasswordHash, password);

        if (passwordResult != PasswordVerificationResult.Success)
            return Invalid<User>("Either Username or password is incorrect");

        CurrentUser = userResult.Value;
        _messenger.Send(new LoggedInUserChangedMessage(CurrentUser));
        return userResult;
    }

    public User? CurrentUser { get; private set; } = null;
    public bool IsAuthenticated => CurrentUser != null;

    public async Task<RegistrationResult> RegisterAsync(LoginDto loginDto)
    {
        if(loginDto.Password != loginDto.ConfirmedPassword)
        {
            return RegistrationResult.PasswordsDoNotMatch;
        }

        if(await _userService.IsEmailExistsAsync(loginDto.Email))
        {
            return RegistrationResult.EmailAlreadyExists;
        }

        if (await _userService.IsUserNameExistsAsync(loginDto.UserName))
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

        await _userService.CreateAsync(user);
        return RegistrationResult.Success;
    }
}