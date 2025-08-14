using Serilog;
using Store.API.Extensions;
using Store.Infrastructure.Data.Postgres;
using Path = System.IO.Path;

namespace Store.API;

public static class Program
{
    public static void Main(string[] args)
    {
        DotNetEnv.Env.Load(Path.Combine(Environment.CurrentDirectory, "..", "..", ".env"));
        
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables();

        Log.Logger.SetUpLogger(builder.Configuration);

        builder.Host.UseSerilog(Log.Logger);

        builder.Services.AddDbContext<AppDbContext>();
        builder.Services.AddServices();
        builder.Services.ConfigureGraphQl();
        builder.Services.RegisterHandlersFromApp();

        var app = builder.Build();

        Log.Debug("Setting up midleware");

        app.UseSerilogRequestLogging();
        app.UseRouting();

        Log.Debug("Midleware setted up succesfuly");
        
        app.MapGraphQL();
        
        app.Run();
    }
}