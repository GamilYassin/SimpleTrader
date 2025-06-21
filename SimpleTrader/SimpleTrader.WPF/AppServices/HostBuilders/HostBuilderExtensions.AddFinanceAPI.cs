using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.WPF.Features.Financials.Models;

namespace SimpleTrader.WPF.AppServices.HostBuilders;

public static partial class HostBuilderExtensions
{
    public static IHostBuilder AddFinanceAPI(this IHostBuilder host)
    {
        host.ConfigureServices((context, services) =>
        {
            var apiKey = context.Configuration.GetValue<string>("FINANCE_API_KEY");
            services.AddSingleton(new FinancialModelingPrepApiKey(apiKey!));

            services.AddHttpClient<FinancialModelingPrepHttpClient>(c =>
            {
                c.BaseAddress = new Uri("https://financialmodelingprep.com/api/v3/");
            });
        });

        return host;
    }
}