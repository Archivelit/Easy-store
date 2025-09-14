namespace Store.API.Extensions;

using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        Log.Debug("Setting up services");

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IUserDao, UserDao>();
        services.AddScoped<IItemDao, ItemDao>();

        services.RegisterUpdateUserServices();
        services.RegisterUpdateItemServices();
        services.RegisterAuthorizationHandlers();

        services.AddTransient<IValidator<User>, UserValidator>();
        services.AddTransient<IValidator<Item>, ItemValidator>();

        services.AddTransient<IItemFactory, ItemDomainFactory>();
        services.AddTransient<IItemEntityFactory, ItemEntityFactory>();

        Log.Debug("Services setted up succesfuly");

        return services;
    }

#nullable disable
    public static IServiceCollection RegisterHandlersFromApp(this IServiceCollection services)
    {
        Log.Debug("Registering handlers");

        var assembly = Assembly.GetAssembly(typeof(RegisterUserCommandHandler));
        var types = assembly.GetTypes();

        var handlerInterfaces = new[]
        {
            typeof(IQueryHandler<,>),
            typeof(ICommandHandler<>),
            typeof(ICommandHandler<,>)
        };

        foreach (var type in types)
        {
            if (!type.IsClass || type.IsAbstract)
                continue;

            var interfaces = type.GetInterfaces()
                .Where(i => i.IsGenericType &&
                            handlerInterfaces.Any(h => h == i.GetGenericTypeDefinition()));
            
            foreach (var iface in interfaces)
            {
                services.AddScoped(iface, type);
                Log.Debug("Adding scoped service {TypeName}, implements {InterfaceName}", type.Name, iface.Name);
            }
        }

        Log.Debug("Handlers registered succesfuly");
        return services;
    }
#nullable enable

    public static IServiceCollection ConfigureRedis(this IServiceCollection services, ConfigurationManager configuration)
    {
        return services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("RedisConnection");
        });
    }

    public static IServiceCollection ConfigureReverseProxy(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddReverseProxy()
            .LoadFromConfig(configuration.GetSection("ReverseProxy"));

        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            options.KnownProxies.Add(IPAddress.Parse("127.0.0.1"));
        });

        return services;
    }

    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, ConfigurationManager configuration)
    {
        #region JWT Bearer configuration
        //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        //    {
        //        options.Authority = configuration["Keycloak:Authority"];
        //        options.Audience = configuration["Keycloak:Audience"];
        //        options.RequireHttpsMetadata = false; // only for dev environment
        //        options.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidIssuer = configuration["Keycloak:ValidIssuer"],
        //            ValidateAudience = false,
        //            ValidateLifetime = false, // only for dev environment
        //            ValidateIssuerSigningKey = false, // only for dev environment
        //            ValidateIssuer = false // only for dev environment
        //        };
        //    });
        #endregion
        services.AddKeycloakWebApiAuthentication(configuration);

        return services;
    }

    public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => 
                policy.RequireRole("Admin"));
            options.AddPolicy("User", policy => 
                policy.RequireRole("User"));
            options.AddPolicy("ItemOwner", policy =>
                policy.Requirements.Add(new ItemOwnerRequirement()));
        });

        return services;
    }

    private static void RegisterUpdateUserServices(this IServiceCollection services)
    {
        services.AddTransient<IUserUpdateChainFactory, UserUpdateChainFactory>();

        services.AddTransient<UserUpdateChainFactory>();
        services.AddTransient<UpdateUserEmail>();
        services.AddTransient<UpdateUserName>();
        services.AddTransient<UpdateUserSubscription>();

        services.AddScoped<UserUpdateFacade>();
    }

    private static void RegisterUpdateItemServices(this IServiceCollection services)
    {
        services.AddTransient<IItemUpdateChainFactory, ItemUpdateChainFactory>();

        services.AddTransient<UpdateTitle>();
        services.AddTransient<UpdateDescription>();
        services.AddTransient<UpdatePrice>();
        services.AddTransient<UpdateQuantity>();
        services.AddTransient<RefreshUpdatedAt>();

        services.AddScoped<ItemUpdateFacade>();
    }

    private static void RegisterAuthorizationHandlers(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationHandler, ItemOwnerHandler>();
    }
}