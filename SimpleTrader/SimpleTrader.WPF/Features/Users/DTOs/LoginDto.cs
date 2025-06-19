namespace SimpleTrader.WPF.Features.Users.DTOs;

public record LoginDto(string UserName, string Email, string Password, string ConfirmedPassword);
