using System.Threading.Tasks;
using FieldOps.Kernel.Functional;
using SimpleTrader.WPF.Features.Assets.Enums;
using SimpleTrader.WPF.Features.Assets.Models;

namespace SimpleTrader.WPF.Features.Financials.Services;

public interface IMajorIndexService
{
    Task<Validation<MajorIndex>> GetMajorIndex(MajorIndexType indexType);
}