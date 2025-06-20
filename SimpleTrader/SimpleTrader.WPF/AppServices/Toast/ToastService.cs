#region

using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using FieldOps.Kernel.Functional;

#endregion

namespace SimpleTrader.WPF.AppServices.Toast;

public class ToastService : IToastService
{
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly SemaphoreSlim _semaphore; // Ensures only one toast is processed at a time

    private readonly ConcurrentQueue<ToastRequest> _toastQueue;

    public ToastService()
    {
        // Start the background processing task
        _toastQueue = new ConcurrentQueue<ToastRequest>();
        _cancellationTokenSource = new CancellationTokenSource();
        _semaphore = new SemaphoreSlim(1, 1);
        Task.Run(ProcessQueueAsync, _cancellationTokenSource.Token);
    }

    public Task ErrorAsync(string message, int durationMs = 2500)
    {
        return EnqueueToast(message, ToastType.Error, durationMs);
    }

    public Task InfoAsync(string message, int durationMs = 2500)
    {
        return EnqueueToast(message, ToastType.Info, durationMs);
    }

    public Task SuccessAsync(string message, int durationMs = 2500)
    {
        return EnqueueToast(message, ToastType.Success, durationMs);
    }

    public Task WarningAsync(string message, int durationMs = 2500)
    {
        return EnqueueToast(message, ToastType.Warning, durationMs);
    }

    public Task ShowResultAsync<T>(Validation<T> result, string operation, string successMsg,
        int durationMs = 2500)
    {
        return result.IsValid
            ? SuccessAsync(successMsg, durationMs)
            : ErrorAsync(result.Error!.Message);
    }

    private Task EnqueueToast(string message, ToastType type, int durationMs)
    {
        ToastRequest request = new(message, type, durationMs);
        _toastQueue.Enqueue(request);
        return Task.CompletedTask; // Enqueueing is synchronous
    }

    private async Task ProcessQueueAsync()
    {
        while (!_cancellationTokenSource.IsCancellationRequested)
        {
            await _semaphore.WaitAsync(_cancellationTokenSource.Token); // Wait for previous toast to finish

            try
            {
                if (_toastQueue.TryDequeue(out ToastRequest? request))
                {
                    // Ensure UI operations are on the UI thread
                    await Application.Current.Dispatcher.InvokeAsync(async () =>
                    {
                        ToastWindow toastWindow = new(request.Message, request.Type, request.DurationMs);
                        await toastWindow.ShowAndAwaitCloseAsync();
                    });
                }
                else
                {
                    // If queue is empty, wait a bit before checking again
                    await Task.Delay(100, _cancellationTokenSource.Token);
                }
            }
            catch (OperationCanceledException)
            {
                // Task was cancelled, exit loop
                break;
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., using a logging framework)
                Console.WriteLine($"Error processing toast: {ex.Message}");
            }
            finally
            {
                _semaphore.Release(); // Allow next toast to be processed
            }
        }
    }

    // Optional: Call this when your application is shutting down
    public void Dispose()
    {
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
        _semaphore.Dispose();
    }

    private class ToastRequest(string message, ToastType type, int durationMs)
    {
        public string Message { get; } = message;
        public ToastType Type { get; } = type;
        public int DurationMs { get; } = durationMs;
    }
}