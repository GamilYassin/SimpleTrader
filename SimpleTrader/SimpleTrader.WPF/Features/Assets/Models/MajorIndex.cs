using FieldOps.Kernel.Entities;

namespace SimpleTrader.WPF.Features.Assets.Models;

public class MajorIndex : EntityBase
{
    public required string IndexName { get; set; }
    public required string UriSuffix { get; set; }
    public decimal Price { get; set; }
    public decimal Changes { get; set; }
}