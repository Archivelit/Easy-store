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
        builder.Services.ConfigureAuthorization(builder.Configuration);
        builder.Services.AddDbContext<AppDbContext>();
        builder.Services.AddMinio(MinIO.ACCESS_KEY, MinIO.SECRET_KEY);
        builder.Services.AddServices();
        builder.Services.AddControllers();
        builder.Services.RegisterHandlersFromApp();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwagger();

        var app = builder.Build();

        Log.Debug("Setting up middleware");

        app.UseForwardedHeaders();
        app.UseSerilogRequestLogging();
        app.UseRouting();
        app.UseSwaggerInDev();
        app.UseAuthentication();
        app.UseAuthorization();

        Log.Debug("Middleware setted up successfully");

        app.MapReverseProxy();
        app.MapControllers();

        await app.Services.MigrateDatabaseAsync();

        await app.RunAsync();
    }
}