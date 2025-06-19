using System;
using System.Threading.Tasks;
using FieldOps.Kernel.Functional;
using SimpleTrader.WPF.AppServices.Exceptions;
using SimpleTrader.WPF.Features.Accounts.Models;
using SimpleTrader.WPF.Features.Users.DTOs;
using SimpleTrader.WPF.Features.Users.Enums;

namespace SimpleTrader.WPF.Features.Users.Services;

public interface IAuthenticationService
{
    /// <summary>
    /// Register a new user.
    /// </summary>
    /// <param name="loginDto"></param>
    /// <returns>The result of the registration.</returns>
    /// <exception cref="Exception">Thrown if the registration fails.</exception>
    Task<RegistrationResult> RegisterAsync(LoginDto loginDto);

    /// <summary>
    /// Get an account for a user's credentials.
    /// </summary>
    /// <param name="username">The user's name.</param>
    /// <param name="password">The user's password.</param>
    /// <returns>The account for the user.</returns>
    /// <exception cref="UserNotFoundException">Thrown if the user does not exist.</exception>
    /// <exception cref="InvalidPasswordException">Thrown if the password is invalid.</exception>
    /// <exception cref="Exception">Thrown if the login fails.</exception>
    Task<Validation<Account>> LoginAsync(string username, string password);
}