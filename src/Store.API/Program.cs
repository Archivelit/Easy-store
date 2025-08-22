using Serilog;
using Store.API.Extensions;
using Store.Infrastructure.Data;
using Path = System.IO.Path;

namespace Store.API;

public static class Program
{
    public static void Main(string[] args)
    {
        DotNetEnv.Env.Load(Path.Combine(Environment.CurrentDirectory, "..", "..", ".env"));
        
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables();
        builder.Configuration.ConfigureLogger();
        
        builder.Host.UseSerilog();

        builder.Services.ConfigureRedis(builder.Configuration);
        builder.Services.ConfigureReverseProxy(builder.Configuration);
        builder.Services.ConfigureAuthentication();
        builder.Services.ConfigureAuthorization();
        builder.Services.AddDbContext<AppDbContext>();
        builder.Services.AddServices();
        builder.Services.ConfigureGraphQl();
        builder.Services.RegisterHandlersFromApp();

        var app = builder.Build();

        Log.Debug("Setting up midleware");

        app.UseSerilogRequestLogging();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        Log.Debug("Midleware setted up succesfuly");

        app.MapReverseProxy();
        app.MapGraphQL();

        app.Run();
    }
}