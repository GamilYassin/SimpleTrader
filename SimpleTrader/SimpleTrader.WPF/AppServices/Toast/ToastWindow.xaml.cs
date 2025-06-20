#region

using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

#endregion

namespace SimpleTrader.WPF.AppServices.Toast;

public partial class ToastWindow : Window
{
    private readonly int _durationMs;
    private readonly TaskCompletionSource<bool> _tcs = new();

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
        Rect workArea = SystemParameters.WorkArea;
        Left = workArea.Right - ActualWidth - 20; // 20px margin from right
        Top = workArea.Bottom - ActualHeight - 20; // 20px margin from bottom

        // Fade in animation
        DoubleAnimation fadeInAnimation = new() { From = 0, To = 1, Duration = TimeSpan.FromMilliseconds(300) };
        BeginAnimation(OpacityProperty, fadeInAnimation);

        // Set up timer for auto-closing
        DispatcherTimer timer = new() { Interval = TimeSpan.FromMilliseconds(_durationMs) };
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
        DoubleAnimation fadeOutAnimation = new() { From = 1, To = 0, Duration = TimeSpan.FromMilliseconds(300) };
        fadeOutAnimation.Completed += (s, args) =>
        {
            // Ensure the window is closed on the UI thread
            Dispatcher.Invoke(() =>
            {
                Close();
                _tcs.TrySetResult(true); // Signal completion
            });
        };
        BeginAnimation(OpacityProperty, fadeOutAnimation);
    }

    // Public method to show the toast and await its completion
    public Task ShowAndAwaitCloseAsync()
    {
        Show();
        return Task.CompletedTask;
    }

    private void ToastWindow_MouseDown(object sender, MouseButtonEventArgs e)
    {
        // Allow clicking the toast to close it immediately
        CloseToast();
    }
}