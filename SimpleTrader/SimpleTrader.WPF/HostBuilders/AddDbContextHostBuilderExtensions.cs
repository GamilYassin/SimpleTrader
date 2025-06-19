using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;
using SimpleTrader.WPF.Data;

namespace SimpleTrader.WPF.HostBuilders;

public static class AddDbContextHostBuilderExtensions
{
    public static IHostBuilder AddDbContext(this IHostBuilder host)
    {
        host.ConfigureServices((context, services) =>
        {
            string connectionString = context.Configuration.GetConnectionString("sqlite");
            Action<DbContextOptionsBuilder> configureDbContext = o => o.UseSqlite(connectionString);

            services.AddDbContext<AppDbContext>(configureDbContext);
            services.AddSingleton<AppDbContextFactory>(new AppDbContextFactory(configureDbContext));
        });

        return host;
    }
}