using Store.Core.Contracts.Repositories;
using Store.Core.Models;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Token;

namespace Data.Postgres;

public class CustomersDb : ICustomerRepository
{
    private readonly string _connectionString;
    private readonly IVaultClient _vaultClient;
    
    public CustomersDb(IVaultClient vaultClient)
    {
        _vaultClient = vaultClient;

        _connectionString = "adfs";
    }
    
    public async Task RegisterAsync(Customer customer, string passwordHash)
    {
        
    }

    public Task<bool> IsEmailClaimed(string email)
    {
        throw new();
    }

    public Task<(string email, string passwordHash)> GetCustomerPasswordHashByEmail(string email)
    {
        throw new();
    }

    public Task<Customer> GetCustomerByEmail(string email)
    {
        throw new ();
    }
}