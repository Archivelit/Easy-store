using StackExchange.Redis;

namespace Store.API.Extensions;

#nullable disable
public static class IServiceCollectionExtensions
{
    private static readonly Assembly _appAssembly;

    static IServiceCollectionExtensions()
    {
        _appAssembly = Assembly.GetAssembly(typeof(IAppAssemblyMarker));
    }

    /// <summary>
    /// Adds all application services to the DI container.
    /// <para> CQRS Handler are registered via MediatR. </para>
    /// </summary>>
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

    /// <summary>
    /// Registers MediatR and all CQRS Handlers from application assembly.
    /// </summary>
    public static IServiceCollection ConfigureMediatR(this IServiceCollection services)
    {
        return services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblies(_appAssembly);
        });
    }

    /// <summary>
    /// Adding redis cache to the DI container.
    /// </summary>
    public static IServiceCollection ConfigureRedis(this IServiceCollection services, ConfigurationManager configuration)
    {
        return services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("RedisConnection");
        });
    }

    /// <summary>
    /// Adding DbContext to the DI container with PostgreSQL provider.
    /// </summary>
    public static IServiceCollection ConfigureDbContext(this IServiceCollection services, ConfigurationManager configuration)
    {
        return services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
    }

    /// <summary>
    /// Configuring YARP reverse proxy.
    /// </summary>
    public static IServiceCollection ConfigureReverseProxy(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddReverseProxy()
            .LoadFromConfig(configuration.GetSection("ReverseProxy"));

        return services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            options.KnownProxies.Add(IPAddress.Parse("127.0.0.1"));
        });
    }

    /// <summary>
    /// Configuring authentication with Keycloak.
    /// </summary>
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

    /// <summary>
    /// Configuring authentication with Keycloak and adds authorization policies.
    /// </summary>
    public static IServiceCollection ConfigureAuthorization(this IServiceCollection services, ConfigurationManager configuration)
    {
        return services.AddAuthorization(options =>
        {
            options.AddPolicy("Administrator", policy =>
                policy.RequireRealmRoles(Roles.ADMINISTRATOR));

            options.AddPolicy("User", policy =>
                policy.RequireRealmRoles(Roles.USER));

            options.AddPolicy("UserOrAdministrator", policy =>
                policy
                    .RequireRealmRoles(Roles.ADMINISTRATOR)
                    .RequireRealmRoles(Roles.USER));

            //  options.AddPolicy("Me", policy =>
            //      policy.Requirements.Add());

            options.AddPolicy("ItemOwner", policy =>
                policy.Requirements.Add(new ItemOwnerRequirement()));

        }).AddKeycloakAuthorization(configuration);
    }

    /// <summary>
    /// Adding Swagger Documentation generator.
    /// </summary>
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        return services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Easy store API",
                Description = "Easy api for e-commerce",
                Contact = new OpenApiContact
                {
                    Name = "Archivelit",
                    Url = new Uri("https://github.com/Archivelit")
                },
                License = new OpenApiLicense
                {
                    Name = "MIT",
                    Url = new Uri("https://opensource.org/license/mit")
                }
            });
        });
    }
    
    /// <summary>
    /// Register services for updating user data via Chain of Responsibility pattern.
    /// </summary>
    private static void RegisterUpdateUserServices(this IServiceCollection services)
    {
        services.AddTransient<IUserUpdateChainFactory, UserUpdateChainFactory>();

        services.AddTransient<UserUpdateChainFactory>();
        services.AddTransient<UpdateUserEmail>();
        services.AddTransient<UpdateUserName>();
        services.AddTransient<UpdateUserSubscription>();

        services.AddScoped<UserUpdateFacade>();
    }

    /// <summary>
    /// Register services for updating item data via Chain of Responsibility pattern.
    /// </summary>
    private static void RegisterUpdateItemServices(this IServiceCollection services)
    {
        services.AddTransient<IItemUpdateChainFactory, ItemUpdateChainFactory>();

        services.AddTransient<UpdateTitle>();
        services.AddTransient<UpdateDescription>();
        services.AddTransient<UpdatePrice>();
        services.AddTransient<UpdateQuantity>();

        services.AddScoped<ItemUpdateFacade>();
    }

    /// <summary>
    /// Registers authorization handlers for policies.
    /// </summary>
    private static void RegisterAuthorizationHandlers(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationHandler, ItemOwnerHandler>();
    }
}