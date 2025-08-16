using Store.App.CQRS.Users.Commands.Update.UpdateChain;
using Store.App.GraphQl;
using Store.App.GraphQl.CQRS;
using Store.App.GraphQl.Users;
using Store.App.GraphQl.Factories;
using Store.Core.Managers;
using Store.Core.Services.Customers;
using Store.Core.Services.Validation;
using Store.App.GraphQl.Validation;
using Store.Core.Providers.Validators;
using Store.App.GraphQl.Security;
using Store.Core.Factories;
using Store.Core.Utils.Hashers;
using Store.Core.Utils.Validators.User;
using Store.Core.Utils.Validators.Items;
using Store.Infrastructure.Contracts;
using Store.Infrastructure.Factories;
using Serilog;

namespace Store.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        Log.Debug("Setting up services");

        services.AddScoped<IItemFactory, ItemFactory>();
        services.AddScoped<IItemEntityFactory, ItemEntityFactory>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddScoped<IEmailValidator, EmailValidatorAdapter>();
        services.AddTransient<IUserNameValidator, CustomerNameValidator>();
        services.AddScoped<IPasswordValidator, PasswordValidator>();
        services.AddScoped<ISubscriptionValidator, SubscriptionValidator>();

        services.AddScoped<IItemValidator, ItemValidator>();
        services.AddScoped<IUserValidator, ValidationService>();

        //services.AddScoped<IUserRepository, UserRepository>();
        //services.AddScoped<IItemRepository, ItemRepository>();

        services.AddScoped<IJwtManager, JwtHandler>();

        services.AddScoped<IUserManager, UserManager>();

        services.AddTransient<UpdateUserEmail>();
        services.AddTransient<UpdateUserName>();
        services.AddTransient<UpdateUserSubscription>();
        
        services.AddTransient<UserUpdateChainFactory>();

        Log.Debug("Services setted up succesfuly");

        return services;
    }


    public static IServiceCollection ConfigureGraphQl(this IServiceCollection services)
    {
        Log.Debug("Configuring GraphQl");

        services
            .AddGraphQLServer()
            .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
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
}
