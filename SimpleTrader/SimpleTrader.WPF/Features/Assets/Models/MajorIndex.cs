using FieldOps.Kernel.Entities;
using SimpleTrader.WPF.Features.Assets.Enums;

namespace SimpleTrader.WPF.Features.Assets.Models;

public class MajorIndex : EntityBase
{
    public string IndexName { get; set; }
    public double Price { get; set; }
    public double Changes { get; set; }
    public MajorIndexType Type { get; set; }
}