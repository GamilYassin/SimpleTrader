using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.FinancialModelingPrepAPI.Services;
using SimpleTrader.WPF.Data.Repositories;
using SimpleTrader.WPF.Domain.Models;
using SimpleTrader.WPF.Domain.Services;
using SimpleTrader.WPF.Domain.Services.Abstractions;

namespace SimpleTrader.WPF.HostBuilders;

public static class AddServicesHostBuilderExtensions
{
    public static IHostBuilder AddServices(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
        {
            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IRepository<Account>, AccountService>();
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IStockPriceService, StockPriceService>();
            services.AddSingleton<IBuyStockService, BuyStockService>();
            services.AddSingleton<ISellStockService, SellStockService>();
            services.AddSingleton<IMajorIndexService, MajorIndexService>();
        });

        return host;
    }
}