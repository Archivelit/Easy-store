using Store.API.Utils.Validators;
using Store.Core.Contracts.Customers;
using Store.Core.Managers;
using Store.Core.Contracts.Repositories;
using Store.Core.Services.Customers;
using Store.Core.Services.Validation;
using Store.Core.Contracts.Validation;
using Store.Core.Providers.Validators;
using Store.Core.Contracts.Security;
using Store.Core.Utils.Hashers;
using Store.Core.Utils.Validators.Customer;
using Store.Core.Contracts.Items;
using Store.Infrastructure.Data.Postgres;

namespace Store.API;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        
        services.AddScoped<ICustomerValidator, ValidationService>();
        services.AddScoped<IEmailValidator, EmailValidatorAdapter>();
        services.AddScoped<ICustomerNameValidator, CustomerNameValidator>();
        services.AddScoped<IPasswordValidator, PasswordValidator>();
        services.AddScoped<ISubscriptionValidator, SubscriptionValidator>();
        services.AddScoped<IItemDtoValidator, ItemDtoValidator>();
        
        services.AddScoped<ICustomerRepository, CustomersDb>();
        
        services.AddScoped<IJwtManager, JwtHandler>();
        
        services.AddScoped<ICustomerManager, CustomersManager>();
        
        return services;
    }
}