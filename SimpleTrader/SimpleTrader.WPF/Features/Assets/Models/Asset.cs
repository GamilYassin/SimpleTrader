using System.ComponentModel.DataAnnotations;
using FieldOps.Kernel.Entities;

namespace SimpleTrader.WPF.Features.Assets.Models;

public class Asset : EntityBase
{
    public required string Symbol { get; set; }
    public required string CompanyName { get; set; }
    public decimal PricePerShare { get; set; }
}