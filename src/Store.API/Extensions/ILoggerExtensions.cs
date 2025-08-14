using Serilog;
using ILogger = Serilog.ILogger;

namespace Store.API.Extensions;

public static class ILoggerExtensions
{
    public static void SetUpLogger(this ILogger logger, ConfigurationManager configuration)
    {
        logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .CreateLogger();
    }
}