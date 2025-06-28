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
        
        Console.WriteLine(Path.Combine(Environment.CurrentDirectory, "..", "..", ".env"));
        
        Console.WriteLine(DotNetEnv.Env.GetString("POSTGRES_CONNECTION_STRING"));
        
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Configuration
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables();
        
        builder.Services.AddDbContext<AppDbContext>();
        builder.Services.AddServices();
        builder.Services.ConfigureMediatR();
        builder.Services.AddControllers();

        var app = builder.Build();

        app.UseLogging();
        app.UseRouting();
        app.MapControllers();
        
        app.Run();
    }
}