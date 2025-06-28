using Store.App.Customers.Commands;
using Store.App.Items.Commands;
using Store.App.Items.Queries;
using Store.Core.Contracts.CQRS.Customers.Commands;
using Store.Core.Contracts.CQRS.Items.Commands;
using Store.Core.Contracts.CQRS.Items.Queries;
using Store.Core.Contracts.Customers;
using Store.Core.Contracts.Items;
using Store.Core.Managers;
using Store.Core.Contracts.Repositories;
using Store.Core.Services.Customers;
using Store.Core.Services.Validation;
using Store.Core.Contracts.Validation;
using Store.Core.Providers.Validators;
using Store.Core.Contracts.Security;
using Store.Core.Factories;
using Store.Core.Utils.Hashers;
using Store.Core.Utils.Validators.Customer;
using Store.Core.Utils.Validators.Items;
using Store.Infrastructure.Contracts;
using Store.Infrastructure.Data.Postgres;
using Store.Infrastructure.Factories;

namespace Store.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IItemFactory, ItemFactory>();
        services.AddScoped<IItemEntityFactory, ItemEntityFactory>();
        
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        
        services.AddScoped<IEmailValidator, EmailValidatorAdapter>();
        services.AddScoped<ICustomerNameValidator, CustomerNameValidator>();
        services.AddScoped<IPasswordValidator, PasswordValidator>();
        services.AddScoped<ISubscriptionValidator, SubscriptionValidator>();
        
        services.AddScoped<IItemValidator, ItemValidator>();
        services.AddScoped<ICustomerValidator, ValidationService>();
        
        services.AddScoped<ICustomerRepository, CustomersRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        
        services.AddScoped<IJwtManager, JwtHandler>();
        
        services.AddScoped<ICustomerManager, CustomersManager>();
        
        return services;
    }

    public static void ConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(
            typeof(RegisterCustomerCommand).Assembly,
            typeof(RegisterCustomerCommandHandler).Assembly,
            typeof(AuthenticateCustomerCommand).Assembly,
            typeof(AuthenticateCustomerCommandHandler).Assembly,
            typeof(CreateItemCommand).Assembly,
            typeof(CreateItemCommandHandler).Assembly,
            typeof(DeleteItemCommand).Assembly,
            typeof(DeleteItemCommandHandler).Assembly,
            typeof(GetItemByIdQuery).Assembly,
            typeof(GetItemByIdQueryHandler).Assembly
        ));
    }
}