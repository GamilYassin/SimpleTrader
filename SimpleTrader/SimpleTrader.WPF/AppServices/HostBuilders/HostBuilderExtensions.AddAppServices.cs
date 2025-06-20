using FieldOps.Kernel.PasswordService;
using FieldOps.Kernel.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.WPF.AppServices.Toast;
using SimpleTrader.WPF.Data.Repositories;
using SimpleTrader.WPF.Features.Accounts.Services;
using SimpleTrader.WPF.Features.Assets.Services;
using SimpleTrader.WPF.Features.Financials.Services;
using SimpleTrader.WPF.Features.Users.Services;

namespace SimpleTrader.WPF.AppServices.HostBuilders;

public static partial class HostBuilderExtensions
{
    public static IHostBuilder AddAppServices(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
        {
            // App Services
            services.AddSingleton<IPasswordHasher, PasswordHasher>()
                .AddSingleton<ISettingsService, SettingsService>()
                .AddSingleton<IToastService, ToastService>();
            
        });

        return host;
    }
}