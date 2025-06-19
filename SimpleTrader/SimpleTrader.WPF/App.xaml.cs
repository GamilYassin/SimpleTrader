#region

using System;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.WPF.AppServices.HostBuilders;
using SimpleTrader.WPF.Data;
using SimpleTrader.WPF.Features.Assets.DataSeed;

#endregion

namespace SimpleTrader.WPF;

public partial class App : Application
{
    private readonly IHost _host;

    public App()
    {
        _host = CreateHostBuilder().Build();
    }

    public static IHostBuilder CreateHostBuilder(string[] args = null)
    {
        return Host.CreateDefaultBuilder(args)
            .AddConfiguration()
            .AddFinanceAPI()
            .AddDbContext()
            .AddServices()
            .AddStores()
            .AddViewModels()
            .AddViews();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _host.Start();

        var contextFactory = _host.Services.GetRequiredService<IDbContextFactory<AppDbContext>>();
        using (var context = contextFactory.CreateDbContext())
        {
            context.Database.Migrate();
        }

        // Seed Data
        SeedData(_host.Services);

        // Window window = _host.Services.GetRequiredService<MainWindow>();
        // window.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await _host.StopAsync();
        _host.Dispose();

        base.OnExit(e);
    }

    private async void SeedData(IServiceProvider service)
    {
        await new AssetSeeder(service).SeedAsync();
        await new MajorIndexSeeder(service).SeedAsync();
    }
}