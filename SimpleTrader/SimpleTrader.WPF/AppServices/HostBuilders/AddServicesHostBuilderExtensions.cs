using FieldOps.Kernel.PasswordService;
using FieldOps.Kernel.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.WPF.Data.Repositories;
using SimpleTrader.WPF.Features.Accounts.Services;
using SimpleTrader.WPF.Features.Assets.Services;
using SimpleTrader.WPF.Features.Financials.Services;
using SimpleTrader.WPF.Features.Users.Services;

namespace SimpleTrader.WPF.AppServices.HostBuilders;

public static class AddServicesHostBuilderExtensions
{
    public static IHostBuilder AddServices(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
        {
            // App Services
            services.AddSingleton<IPasswordHasher, PasswordHasher>()
                .AddSingleton<ISettingsService, SettingsService>();
            
            // Generic Services
            services.AddSingleton(typeof(IRepository<>), typeof(GenericRepository<>));
            
            // Domain Services
            services.AddSingleton<IAuthenticationService, AuthenticationService>()
                .AddSingleton<IAccountService, AccountService>()
                .AddSingleton<IStockPriceService, StockPriceService>()
                .AddSingleton<IBuyStockService, BuyStockService>()
                .AddSingleton<ISellStockService, SellStockService>();
            // .AddSingleton<IMajorIndexService, MajorIndexService>();
        });

        return host;
    }
}