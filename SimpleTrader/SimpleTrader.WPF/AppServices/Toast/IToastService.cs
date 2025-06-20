#region

using System.Threading.Tasks;
using FieldOps.Kernel.Functional;

#endregion

namespace SimpleTrader.WPF.AppServices.Toast;

public interface IToastService
{
    Task ErrorAsync(string message, int durationMs = 2500);
    Task InfoAsync(string message, int durationMs = 2500);
    Task SuccessAsync(string message, int durationMs = 2500);
    Task WarningAsync(string message, int durationMs = 2500);
    Task ShowResultAsync<T>(Validation<T> result, string operation, string successMsg, int durationMs = 2500);
}