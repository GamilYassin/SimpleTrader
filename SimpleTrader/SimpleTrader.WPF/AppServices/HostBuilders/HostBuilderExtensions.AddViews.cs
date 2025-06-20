using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.WPF.Features.Home.Views;

namespace SimpleTrader.WPF.AppServices.HostBuilders;

public static partial class HostBuilderExtensions
{
    public static IHostBuilder AddViews(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
        {
            services.AddSingleton<MainWindow>();
        });

        return host;
    }
}