using CommunityToolkit.Mvvm.Messaging;
using FieldOps.Kernel.AppService;
using FieldOps.Kernel.ClipboardWrapper;
using FieldOps.Kernel.Configuration;
using FieldOps.Kernel.FilesIO;
using FieldOps.Kernel.PasswordService;
using FieldOps.Kernel.Serializer;
using FieldOps.Kernel.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.WPF.AppServices.Toast;

namespace SimpleTrader.WPF.AppServices.HostBuilders;

public static partial class HostBuilderExtensions
{
    public static IHostBuilder AddAppServices(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
        {
            // App Services
            services.AddSingleton<IPasswordHasher, PasswordHasher>()
                .AddSingleton<IApplicationService, ApplicationService>()
                .AddSingleton<ISettingsService, SettingsService>()
                .AddSingleton<IClipboardService, ClipboardService>()
                .AddSingleton<IFileDialogService, FileDialogService>()
                .AddSingleton<IToastService, ToastService>()
                .AddSingleton<IConfigurationService>(x => new ConfigurationService(x).Initialize())
                .AddSingleton<ISerializerService, SerializerService>()
                // Messenger
                .AddSingleton<WeakReferenceMessenger>()
                .AddSingleton<IMessenger, WeakReferenceMessenger>(provider =>
                    provider.GetRequiredService<WeakReferenceMessenger>());
        });

        return host;
    }
}