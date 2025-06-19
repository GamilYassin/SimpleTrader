using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.WPF.AppServices.Navigation;
using SimpleTrader.WPF.Features.Accounts.Stores;
using SimpleTrader.WPF.Features.Assets.Stores;
using SimpleTrader.WPF.Features.Users.Stores;

namespace SimpleTrader.WPF.AppServices.HostBuilders;

public static class AddStoresHostBuilderExtensions
{
    public static IHostBuilder AddStores(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
        {
            services.AddSingleton<INavigator, Navigator>();
            services.AddSingleton<IAuthenticator, Authenticator>();
            services.AddSingleton<IAccountStore, AccountStore>();
            services.AddSingleton<AssetStore>();
        });

        return host;
    }
}