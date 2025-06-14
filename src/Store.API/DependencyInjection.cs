using Store.Core.Contracts.Customers;
using Store.Core.Managers;
using Store.Core.Contracts.Repositories;
using Store.Core.Services.Customers;
using Store.Infrastructure.Data.Sqlite.CustomersData;
using Store.Core.Services.Validation;
using Store.Core.Contracts.Validation;
using Store.Core.Providers.Validators;
using Store.Core.Contracts.Security;
using Store.Core.Utils.Hashers;
using Store.Core.Utils.Validators;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Token;

namespace API;

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
        
        services.AddScoped<ICustomerRepository, CustomerAuthentication>();
        
        services.AddScoped<IJwtManager, JwtHandler>();
        
        services.AddScoped<ICustomerManager, CustomersManager>();
        
        IAuthMethodInfo authMethod = new TokenAuthMethodInfo(vaultToken: "dev-only-token");
        
        VaultClientSettings vaultClientSettings = new VaultClientSettings("http://127.0.0.1:8200", authMethod);
        var vaultClient = new VaultClient(vaultClientSettings);
        
        services.AddSingleton<IVaultClient>(vaultClient);
            
        
        return services;
    }
}