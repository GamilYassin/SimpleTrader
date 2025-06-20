using FieldOps.Kernel.AppService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.WPF.Data;

namespace SimpleTrader.WPF.AppServices.HostBuilders;

public static partial class HostBuilderExtensions
{
    public static IHostBuilder AddDbContext(this IHostBuilder host)
    {
        host.ConfigureServices((context, services) =>
        {
            services.AddDbContextFactory<AppDbContext>(options =>
            {
                var connectionString = new ApplicationService().GetDefaultConnectString();
                options.UseSqlite(connectionString);
            });
            
            // string connectionString = context.Configuration.GetConnectionString("sqlite");
            // Action<DbContextOptionsBuilder> configureDbContext = o => o.UseSqlite(connectionString);

            // services.AddDbContext<AppDbContext>(configureDbContext);
            // services.AddSingleton<AppDbContextFactory>(new AppDbContextFactory(configureDbContext));
        });

        return host;
    }
}