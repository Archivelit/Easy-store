using Store.API.Extensions;
using Store.API.Middleware.Logging;
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
        
        builder.Services.AddDbContext<AppDbContext>();
        builder.Services.AddServices();
        builder.Services.ConfigureGraphQl();
        builder.Services.RegisterHandlersFromApp();

        var app = builder.Build();

        app.UseLogging();
        app.UseRouting();
        app.MapGraphQL();
        
        app.Run();
    }
}