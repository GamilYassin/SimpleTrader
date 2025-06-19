using System.Threading.Tasks;
using SimpleTrader.WPF.Features.Assets.Enums;
using SimpleTrader.WPF.Features.Assets.Models;

namespace SimpleTrader.WPF.Features.Assets.Services;

public interface IMajorIndexService
{
    Task<MajorIndex> GetMajorIndex(MajorIndexType indexType);
}