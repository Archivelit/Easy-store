using Store.App.CQRS.Users.Commands.Update.UpdateChain;
using Store.App.GraphQl;
using Store.Core.Managers;
using Store.Core.Services.Customers;
using Store.Core.Services.Validation;
using Store.Core.Providers.Validators;
using Store.Core.Factories;
using Store.Core.Utils.Hashers;
using Store.Core.Utils.Validators.User;
using Store.Core.Utils.Validators.Items;
using Store.Infrastructure.Contracts;
using Store.Infrastructure.Factories;
using Serilog;
using Store.Core.Contracts.Factories;
using Store.Core.Contracts.Repositories;
using Store.Infrastructure.Repositories;
using Store.Infrastructure.Data.DataAccessObjects;
using Store.Core.Contracts.Validation;
using Store.Core.Contracts.Security;
using Store.Core.Contracts.Users;
using Store.Core.Contracts.CQRS;

namespace Store.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        Log.Debug("Setting up services");

        services.AddTransient<IItemFactory, ItemFactory>();
        services.AddTransient<IItemEntityFactory, ItemEntityFactory>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddTransient<IEmailValidator, EmailValidatorAdapter>();
        services.AddTransient<IUserNameValidator, UserNameValidator>();
        services.AddTransient<IPasswordValidator, PasswordValidator>();
        services.AddTransient<ISubscriptionValidator, SubscriptionValidator>();

        services.AddScoped<IItemValidator, ItemValidator>();
        services.AddScoped<IUserValidator, ValidationService>();

        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IItemRepository, ItemRepository>();

        services.AddScoped<IJwtManager, JwtHandler>();
        services.AddScoped<IUserManager, UserManager>();

        services.AddTransient<UpdateUserEmail>();
        services.AddTransient<UpdateUserName>();
        services.AddTransient<UpdateUserSubscription>();
        
        services.AddTransient<UserUpdateChainFactory>();

        services.AddScoped<IUserDao, UserDao>();
        services.AddScoped<IItemDao, ItemDao>();

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
