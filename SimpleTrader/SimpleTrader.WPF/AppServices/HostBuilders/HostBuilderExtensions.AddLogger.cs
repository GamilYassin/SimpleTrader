#region

using FieldOps.Kernel.AppService;
using Microsoft.Extensions.Hosting;
using Serilog;

#endregion

namespace SimpleTrader.WPF.AppServices.HostBuilders;

public static partial class HostBuilderExtensions
{
    public static IHostBuilder AddLogger(this IHostBuilder host)
    {
        host.UseSerilog((context, services, loggerConfiguration) =>
        {
            // Define the log file path
            var logFilePath = new ApplicationService().GetLogFilePath();

            loggerConfiguration
                .ReadFrom.Configuration(context.Configuration) // Read from appsettings.json if configured
                .MinimumLevel.Debug() // Set minimum logging level for all sinks
                .Enrich.FromLogContext() // Allows adding properties to log context
                .Enrich.WithMachineName()
                .Enrich.WithProcessId()
                .Enrich.WithThreadId()
                .WriteTo.Debug() // Output to Visual Studio's Output window (Debug pane)
                .WriteTo.Console() // Output to console if running as console app
                .WriteTo.File(
                    logFilePath, // File path with rolling date
                    rollingInterval: RollingInterval.Day, // Roll log file daily
                    outputTemplate:
                    "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}",
                    fileSizeLimitBytes: 2 * 1024 * 1024, // 10 MB file size limit
                    rollOnFileSizeLimit: true, // Roll when size limit is reached
                    retainedFileCountLimit: 31, // Keep logs for 31 days
                    shared: true // Allow multiple processes to write to the same log file (e.g., if you have multiple app instances)
                );
        });

        return host;
    }
}