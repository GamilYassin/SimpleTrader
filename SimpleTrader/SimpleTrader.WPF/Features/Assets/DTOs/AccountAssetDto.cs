namespace SimpleTrader.WPF.Features.Assets.DTOs;

public class AccountAssetDto
{
    public string? CompanyName { get; set; }
    public string? Symbol { get; set; }
    public decimal AveragePricePerShare { get; set; }
    public int Shares { get; set; }
}