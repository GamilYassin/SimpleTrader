using System.Threading.Tasks;
using SimpleTrader.WPF.Domain.Models;

namespace SimpleTrader.WPF.Domain.Services.Abstractions;

public interface IMajorIndexService
{
    Task<MajorIndex> GetMajorIndex(MajorIndexType indexType);
}