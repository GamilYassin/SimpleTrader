using System.Threading.Tasks;
using SimpleTrader.WPF.Domain.Models;

namespace SimpleTrader.WPF.Domain.Services;

public interface IMajorIndexService
{
    Task<MajorIndex> GetMajorIndex(MajorIndexType indexType);
}