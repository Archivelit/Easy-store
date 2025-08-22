using FluentValidation;
using Serilog;
using Store.App.CQRS.Items.Commands.Update;
using Store.App.CQRS.Items.Commands.Update.UpdateChain;
using Store.App.CQRS.Users.Commands.Update;
using Store.App.CQRS.Users.Commands.Update.UpdateChain;
using Store.App.GraphQl;
using Store.Core.Contracts.CQRS;
using Store.Core.Contracts.Factories;
using Store.Core.Contracts.Repositories;
using Store.Core.Contracts.Users;
using Store.Core.Factories;
using Store.Core.Managers;
using Store.Core.Models;
using Store.Core.Utils.Validators.Items;
using Store.Core.Utils.Validators.User;
using Store.Infrastructure.Contracts;
using Store.Infrastructure.Data.DataAccessObjects;
using Store.Infrastructure.Factories;
using Store.Infrastructure.Repositories;

namespace Store.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        Log.Debug("Setting up services");

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IUserDao, UserDao>();
        services.AddScoped<IItemDao, ItemDao>();
        services.AddScoped<IUserManager, UserManager>();

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
