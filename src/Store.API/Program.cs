using Serilog;
using Store.API.Extensions;
using Store.Infrastructure.Data;
using Path = System.IO.Path;
using Microsoft.IdentityModel.Tokens;

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

        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
        });

        builder.Configuration.ConfigureLogger();

        builder.Host.UseSerilog();

        builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "http://localhost:8080/realms/store";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("default", policy =>
                policy.RequireAuthenticatedUser());
        });

        builder.Services
            .AddReverseProxy()
            .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

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