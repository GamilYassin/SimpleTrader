using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.WPF.Data.Repositories;
using SimpleTrader.WPF.Features.Accounts.Services;
using SimpleTrader.WPF.Features.Assets.Services;
using SimpleTrader.WPF.Features.Financials.Services;
using SimpleTrader.WPF.Features.Users.Services;

namespace SimpleTrader.WPF.AppServices.HostBuilders;

public static partial class HostBuilderExtensions
{
    public static IHostBuilder AddDomainServices(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
        {
            // Generic Services
            services.AddSingleton(typeof(IRepository<>), typeof(GenericRepository<>));
            
            // Domain Services
            services.AddSingleton<IAuthenticationService, AuthenticationService>()
                .AddSingleton<IAccountService, AccountService>()
                .AddSingleton<IStockPriceService, StockPriceService>()
                .AddSingleton<IBuyStockService, BuyStockService>()
                .AddSingleton<IUserService, UserService>()
                .AddSingleton<ISellStockService, SellStockService>();
            // .AddSingleton<IMajorIndexService, MajorIndexService>();
        });

        return host;
    }
}