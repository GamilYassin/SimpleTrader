Okay, let's build a robust WPF Toast Service that implements your `IToastService` interface.

This solution will involve:

1. **`IToastService`**: Your defined interface.
2. **`ToastType` Enum**: To categorize toast messages (Info, Success, Error, Warning) for styling.
3. **`ToastWindow.xaml` / `ToastWindow.xaml.cs`**: A custom WPF `Window` that represents a single toast notification. It
   will handle its own display, animation, and self-closing.
4. **`ToastService`**: The concrete implementation of `IToastService`. It will manage a queue of toast requests and
   display them sequentially to avoid overwhelming the user.
5. **Dependency Injection Setup**: How to register and use the service in your WPF application.

---

### 1. `IToastService.cs` (Your Interface)

```csharp
// In your project: Porteflow.WPF.AppServices.Toast
namespace Porteflow.WPF.AppServices.Toast;

public interface IToastService
{
    Task ErrorAsync( string message , int durationMs = 2500 );
    Task InfoAsync( string message , int durationMs = 2500 );
    Task SuccessAsync( string message , int durationMs = 2500 );
    Task WarningAsync( string message , int durationMs = 2500 );
}
```

---

### 2. `ToastType.cs` (Enum for Toast Categories)

```csharp
// In your project: Porteflow.WPF.AppServices.Toast
namespace Porteflow.WPF.AppServices.Toast;

public enum ToastType
{
    Info,
    Success,
    Warning,
    Error
}
```

---

### 3. `ToastWindow.xaml` (The Visual Toast)

Create a new WPF `Window` item in your project (e.g., in a `Views` or `Controls` folder) and name it `ToastWindow.xaml`.

```xml
<!-- ToastWindow.xaml -->
<Window x:Class="Porteflow.WPF.AppServices.Toast.ToastWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        mc:Ignorable="d"
        Title="Toast"
        WindowStyle="None"
        AllowsTransparency="True"
        Background="Transparent"
        ShowInTaskbar="False"
        Topmost="True"
        SizeToContent="WidthAndHeight"
        Loaded="ToastWindow_Loaded"
        MouseDown="ToastWindow_MouseDown">
    <Border x:Name="ToastBorder"
            CornerRadius="8"
            Padding="15"
            Margin="10"
            MaxWidth="350"
            Background="#333333"
            BorderBrush="#555555"
            BorderThickness="1">
        <TextBlock x:Name="MessageTextBlock"
                   TextWrapping="Wrap"
                   Foreground="White"
                   FontSize="14"
                   FontWeight="SemiBold"
                   Text="This is a toast message."/>
    </Border>
</Window>
```

---

### 4. `ToastWindow.xaml.cs` (Code-behind for Toast Window)

```csharp
// ToastWindow.xaml.cs
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Porteflow.WPF.AppServices.Toast;

public partial class ToastWindow : Window
{
    private readonly int _durationMs;
    private readonly TaskCompletionSource<bool> _tcs = new TaskCompletionSource<bool>();

    public ToastWindow(string message, ToastType type, int durationMs)
    {
        InitializeComponent();
        _durationMs = durationMs;
        MessageTextBlock.Text = message;
        ApplyToastTypeStyle(type);
    }

    private void ApplyToastTypeStyle(ToastType type)
    {
        switch (type)
        {
            case ToastType.Info:
                ToastBorder.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x21, 0x96, 0xF3)); // Blue
                ToastBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x19, 0x76, 0xD2));
                break;
            case ToastType.Success:
                ToastBorder.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x4C, 0xAF, 0x50)); // Green
                ToastBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x38, 0x8E, 0x3C));
                break;
            case ToastType.Warning:
                ToastBorder.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xC1, 0x07)); // Amber
                ToastBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xA0, 0x00));
                MessageTextBlock.Foreground = Brushes.Black; // Make text black for better contrast
                break;
            case ToastType.Error:
                ToastBorder.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF4, 0x43, 0x36)); // Red
                ToastBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xD3, 0x2F, 0x2F));
                break;
            default:
                // Default style (e.g., dark grey)
                break;
        }
    }

    private void ToastWindow_Loaded(object sender, RoutedEventArgs e)
    {
        // Position the toast (bottom-right corner of the work area)
        var workArea = SystemParameters.WorkArea;
        this.Left = workArea.Right - this.ActualWidth - 20; // 20px margin from right
        this.Top = workArea.Bottom - this.ActualHeight - 20; // 20px margin from bottom

        // Fade in animation
        var fadeInAnimation = new DoubleAnimation
        {
            From = 0,
            To = 1,
            Duration = TimeSpan.FromMilliseconds(300)
        };
        this.BeginAnimation(OpacityProperty, fadeInAnimation);

        // Set up timer for auto-closing
        var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(_durationMs) };
        timer.Tick += (s, args) =>
        {
            timer.Stop();
            CloseToast();
        };
        timer.Start();
    }

    private void CloseToast()
    {
        // Fade out animation
        var fadeOutAnimation = new DoubleAnimation
        {
            From = 1,
            To = 0,
            Duration = TimeSpan.FromMilliseconds(300)
        };
        fadeOutAnimation.Completed += (s, args) =>
        {
            // Ensure the window is closed on the UI thread
            Dispatcher.Invoke(() =>
            {
                this.Close();
                _tcs.TrySetResult(true); // Signal completion
            });
        };
        this.BeginAnimation(OpacityProperty, fadeOutAnimation);
    }

    // Public method to show the toast and await its completion
    public Task ShowAndAwaitCloseAsync()
    {
        this.Show();
        return _tcs.Task;
    }

    private void ToastWindow_MouseDown(object sender, MouseButtonEventArgs e)
    {
        // Allow clicking the toast to close it immediately
        CloseToast();
    }
}
```

---

### 5. `ToastService.cs` (The Service Implementation)

This service will queue toast requests and process them one by one to prevent multiple toasts from overlapping or
appearing too quickly.

```csharp
// In your project: Porteflow.WPF.AppServices.Toast
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Porteflow.WPF.AppServices.Toast;

public class ToastService : IToastService
{
    private class ToastRequest
    {
        public string Message { get; }
        public ToastType Type { get; }
        public int DurationMs { get; }

        public ToastRequest(string message, ToastType type, int durationMs)
        {
            Message = message;
            Type = type;
            DurationMs = durationMs;
        }
    }

    private readonly ConcurrentQueue<ToastRequest> _toastQueue = new ConcurrentQueue<ToastRequest>();
    private Task _processingTask;
    private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1); // Ensures only one toast is processed at a time

    public ToastService()
    {
        // Start the background processing task
        _processingTask = Task.Run(ProcessQueueAsync, _cancellationTokenSource.Token);
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

    private Task EnqueueToast(string message, ToastType type, int durationMs)
    {
        var request = new ToastRequest(message, type, durationMs);
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
                if (_toastQueue.TryDequeue(out var request))
                {
                    // Ensure UI operations are on the UI thread
                    await Application.Current.Dispatcher.InvokeAsync(async () =>
                    {
                        var toastWindow = new ToastWindow(request.Message, request.Type, request.DurationMs);
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
}
```

---

### 6. Dependency Injection Setup (e.g., in `App.xaml.cs`)

If you're using a modern .NET 6+ WPF app with `Microsoft.Extensions.Hosting` and
`Microsoft.Extensions.DependencyInjection`, you'd set it up like this:

First, ensure you have the necessary NuGet packages:
`Microsoft.Extensions.Hosting`
`Microsoft.Extensions.DependencyInjection`

**`App.xaml.cs`:**

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Porteflow.WPF.AppServices.Toast; // Adjust namespace as needed
using System.Windows;

namespace Porteflow.WPF.App; // Your app's root namespace

public partial class App : Application
{
    private readonly IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // Register your ToastService as a singleton
                services.AddSingleton<IToastService, ToastService>();

                // Register your main window or other services here
                // services.AddSingleton<MainWindow>();
                // services.AddTransient<MyViewModel>();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await _host.StartAsync();

        // Example: Get the service and show a toast on startup
        var toastService = _host.Services.GetRequiredService<IToastService>();
        toastService.InfoAsync("Welcome to Porteflow!", 3000);

        // If you have a MainWindow, you'd typically show it here
        // var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        // mainWindow.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        // Dispose the ToastService if it has a Dispose method
        if (_host.Services.GetService<IToastService>() is ToastService toastService)
        {
            toastService.Dispose();
        }

        await _host.StopAsync();
        _host.Dispose();
        base.OnExit(e);
    }
}
```

---

### 7. How to Use the `IToastService` in your ViewModels or Code-behind

Now, you can inject `IToastService` into any class that needs to display toasts.

**Example ViewModel:**

```csharp
using CommunityToolkit.Mvvm.ComponentModel; // Or your preferred MVVM framework
using CommunityToolkit.Mvvm.Input;
using Porteflow.WPF.AppServices.Toast;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Porteflow.WPF.App.ViewModels; // Adjust namespace

public partial class MainViewModel : ObservableObject
{
    private readonly IToastService _toastService;

    public MainViewModel(IToastService toastService)
    {
        _toastService = toastService;
    }

    [RelayCommand]
    private async Task ShowInfoToast()
    {
        await _toastService.InfoAsync("This is an info message!");
    }

    [RelayCommand]
    private async Task ShowSuccessToast()
    {
        await _toastService.SuccessAsync("Operation completed successfully!");
    }

    [RelayCommand]
    private async Task ShowWarningToast()
    {
        await _toastService.WarningAsync("Something might be wrong. Please check.");
    }

    [RelayCommand]
    private async Task ShowErrorToast()
    {
        await _toastService.ErrorAsync("An error occurred. Please try again.");
    }

    [RelayCommand]
    private async Task ShowLongToast()
    {
        await _toastService.InfoAsync("This toast will stay for 5 seconds.", 5000);
    }

    [RelayCommand]
    private async Task ShowMultipleToastsQuickly()
    {
        await _toastService.InfoAsync("First message!");
        await _toastService.SuccessAsync("Second message!");
        await _toastService.WarningAsync("Third message!");
        await _toastService.ErrorAsync("Fourth message!");
        // These will appear sequentially due to the queueing in ToastService
    }
}
```

**Example `MainWindow.xaml` (if using the ViewModel):**

```xml
<Window x:Class="Porteflow.WPF.App.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:vm="clr-namespace:Porteflow.WPF.App.ViewModels"
        mc:Ignorable="d"
        Title="Porteflow App" Height="450" Width="800">
    <Window.DataContext>
        <vm:MainViewModel d:IsDataSource="True"/>
    </Window.DataContext>
    <StackPanel HorizontalAlignment="Center" VerticalAlignment="Center">
        <Button Content="Show Info Toast" Command="{Binding ShowInfoToastCommand}" Margin="5"/>
        <Button Content="Show Success Toast" Command="{Binding ShowSuccessToastCommand}" Margin="5"/>
        <Button Content="Show Warning Toast" Command="{Binding ShowWarningToastCommand}" Margin="5"/>
        <Button Content="Show Error Toast" Command="{Binding ShowErrorToastCommand}" Margin="5"/>
        <Button Content="Show Long Toast (5s)" Command="{Binding ShowLongToastCommand}" Margin="5"/>
        <Button Content="Show Multiple Toasts Quickly" Command="{Binding ShowMultipleToastsQuicklyCommand}" Margin="5"/>
    </StackPanel>
</Window>
```

**`MainWindow.xaml.cs` (if using the ViewModel with DI):**

```csharp
using Porteflow.WPF.App.ViewModels; // Adjust namespace
using System.Windows;

namespace Porteflow.WPF.App;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
```

And remember to register `MainWindow` and `MainViewModel` in `App.xaml.cs`'s `ConfigureServices`:

```csharp
// In App.xaml.cs ConfigureServices
services.AddSingleton<MainWindow>(); // If MainWindow is your main window
services.AddTransient<MainViewModel>(); // Or Singleton, depending on your needs
```

This comprehensive setup provides a clean, asynchronous, and robust toast notification system for your WPF application.