namespace Store.API.Extensions;

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

        services.AddTransient<IValidator<User>, UserValidator>();
        services.AddTransient<IValidator<Item>, ItemValidator>();

        services.AddTransient<IItemFactory, ItemDomainFactory>();
        services.AddTransient<IItemEntityFactory, ItemEntityFactory>();

        Log.Debug("Services setted up succesfuly");

        return services;
    }

    public static IServiceCollection ConfigureGraphQl(this IServiceCollection services)
    {
        Log.Debug("Configuring GraphQl");

        services
            .AddGraphQLServer()
            .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
            .AddAuthorization()
            .AddQueryType<Query>()
            .AddMutationType<Mutation>()
            /*.AddSubscriptionType<Subscription>()*/;

        services.AddGraphQlExtendTypes();

        Log.Debug("GraphQl server configured succesfuly");

        return services;
    }

    public static IServiceCollection RegisterHandlersFromApp(this IServiceCollection services)
    {
        Log.Debug("Registering handlers");

        var assembly = typeof(Query).Assembly;
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
            }
        }

        Log.Debug("Handlers registered succesfuly");
        return services;
    }

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

        return services;
    }

    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["Keycloak:Authority"];
                options.Audience = configuration["Keycloak:Audience"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["Keycloak:ValidIssuer"],
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true
                };
            });

        return services;
    }

    public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("authenticated", policy =>
                policy.RequireAuthenticatedUser());
            options.AddPolicy("Admin", policy => 
                policy.RequireRole("Admin"));
            options.AddPolicy("User", policy => 
                policy.RequireRole("User"));
        });

        return services;
    }

    private static IServiceCollection AddGraphQlExtendTypes(this IServiceCollection services)
    {
        Log.Debug("Adding GraphQl extend types");
        var assembly = typeof(Query).Assembly;
        var extenderInterface = typeof(IGraphQlExtender);
        
        var types = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && extenderInterface.IsAssignableFrom(t));


        foreach (var type in types)
        {
            services.AddGraphQLServer().AddTypeExtension(type);
        }

        Log.Debug("GraphQl extend types added succesfuly");
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
}
