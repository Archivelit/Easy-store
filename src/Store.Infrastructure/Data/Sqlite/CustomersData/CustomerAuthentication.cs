using Exceptions;
using Microsoft.Data.Sqlite;
using Store.Core.Contracts.Repositories;
using Store.Core.Enums.Subscriptions;
using Store.Core.Models;

namespace Store.Infrastructure.Data.Sqlite.CustomersData;

internal class CustomerAuthentication : CustomersDb, ICustomerRepository
{
    public async Task RegisterAsync(Customer customer, string passwordHash)
    {
        try
        {
            await using var db = await OpenConnection();

            await using var insertCommand = new SqliteCommand("""
                                                              INSERT INTO Customers 
                                                              (Id, Name, SubscriptionType, Email, PasswordHash) 
                                                              VALUES (@id, @name, @type, @email, @passwordHash)
                                                              """, db);

            insertCommand.Parameters.AddWithValue("@id", customer.Id);
            insertCommand.Parameters.AddWithValue("@name", customer.Name);
            insertCommand.Parameters.AddWithValue("@type", customer.SubscriptionType);
            insertCommand.Parameters.AddWithValue("@email", customer.Email);
            insertCommand.Parameters.AddWithValue("@passwordHash", passwordHash);

            await insertCommand.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<bool> IsEmailClaimed(string email)
    {
        try
        {
            await using var db = await OpenConnection();

            await using var insertCommand = new SqliteCommand("SELECT 1 FROM Customers WHERE Email = @email", db);

            insertCommand.Parameters.AddWithValue("@email", email);

            return await insertCommand.ExecuteScalarAsync() != null;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<(string email, string passwordHash)> GetCustomerPasswordHashByEmail(string email)
    {
        try
        {
            await using var db = await OpenConnection();
            
            await using var command = new SqliteCommand("SELECT Email, PasswordHash FROM Customers WHERE Email = @email LIMIT 1", db);
            
            command.Parameters.AddWithValue("@email", email);
            
            await using var reader = await command.ExecuteReaderAsync();
            
            if (await reader.ReadAsync())
            {
                var passwordHash = reader.GetString(1); // PasswordHash
                return (email, passwordHash);
            }

            throw new CustomerNotFoundException("Customer with specified email not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Customer> GetCustomerByEmail(string email)
    {
        try
        {
            await using var db = await OpenConnection();

            await using var command =
                new SqliteCommand("SELECT Id, Name, SubscriptionType, Email FROM Customers WHERE Email = @email LIMIT 1", db);

            command.Parameters.AddWithValue("@email", email);

            var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                var foundId = reader.GetGuid(0);
                var foundName = reader.GetString(1);
                var foundSubscriptionType = reader.GetString(2);

                return new(foundId, foundName, email, Enum.Parse<Subscription>(foundSubscriptionType));
            }
            
            throw new CustomerNotFoundException("Customer with specified email not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}