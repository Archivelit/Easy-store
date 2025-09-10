namespace Store.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddJsonFile("appsettings.json", optional: false);
        builder.Configuration.ConfigureLogger();
        
        builder.Host.UseSerilog();

        builder.Services.ConfigureRedis(builder.Configuration);
        builder.Services.ConfigureReverseProxy(builder.Configuration);
        builder.Services.ConfigureAuthentication(builder.Configuration);
        builder.Services.ConfigureAuthorization();
        builder.Services.AddDbContext<AppDbContext>();
        builder.Services.AddServices();
        builder.Services.ConfigureGraphQl();
        builder.Services.RegisterHandlersFromApp();

        var app = builder.Build();

        Log.Debug("Setting up middleware");

        app.UseSerilogRequestLogging();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        Log.Debug("Middleware setted up successfully");

        app.MapReverseProxy();
        app.MapGraphQL();

        await app.Services.MigrateDatabaseAsync();

        await app.RunAsync();
    }
}