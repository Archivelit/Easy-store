using Serilog;

namespace Store.API.Extensions;

public static class IConfigurationManagerExtensions
{
    public static void SetUpLogger(this ConfigurationManager configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .CreateLogger();
    }
}