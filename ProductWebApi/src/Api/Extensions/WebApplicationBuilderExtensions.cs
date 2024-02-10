using Serilog;
using Serilog.Events;

namespace ProductWebApi.Api.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddSerilog(this ConfigureHostBuilder host)
    {
        host.UseSerilog();

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(@"C:\log\log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }
}
