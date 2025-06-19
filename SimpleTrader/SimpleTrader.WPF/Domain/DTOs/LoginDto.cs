namespace SimpleTrader.WPF.Domain.DTOs;

public record LoginDto(string UserName, string Email, string Password, string ConfirmedPassword);
