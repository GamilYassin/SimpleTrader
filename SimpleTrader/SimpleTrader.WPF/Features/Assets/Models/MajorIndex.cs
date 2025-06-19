using FieldOps.Kernel.Entities;

namespace SimpleTrader.WPF.Features.Assets.Models;

public class MajorIndex : EntityBase
{
    public string IndexName { get; set; }
    public double Price { get; set; }
    public double Changes { get; set; }
    public MajorIndexType Type { get; set; }
}

public enum MajorIndexType
{
    DowJones,
    Nasdaq,
    SP500
}
