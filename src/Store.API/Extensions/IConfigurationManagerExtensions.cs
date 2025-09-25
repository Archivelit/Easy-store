namespace Store.API.Extensions;

public static class IConfigurationManagerExtensions
{
    /// <summary>
    /// Configure Serilog logger from appsettings.json
    /// </summary>
    public static void ConfigureLogger(this ConfigurationManager configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .CreateLogger();
    }
}