using Store.API.Middleware.Logging;
using Microsoft.EntityFrameworkCore;
using Store.Infrastructure.Data.Postgres;

namespace Store.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
     
        DotNetEnv.Env.Load(Path.Combine(Environment.CurrentDirectory, "..", "..", ".env"));
        var conStr = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");
        
        builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(conStr));
        
        builder.Services.AddServices();
        
        builder.Services.AddControllers();

        var app = builder.Build();

        app.UseLogging();
        app.UseRouting();
        
        app.MapControllers();

        app.Run();
    }
}