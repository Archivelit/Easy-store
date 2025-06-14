using Store.Core.Models;
using Microsoft.Data.Sqlite;
using Store.Infrastructure.Data.Sqlite.CustomersData;

namespace Store.Tests.DataAccess.Sqlite;

internal class InMemoryCustomerDb : CustomersDb
{
    private SqliteConnection _connection;
    
    public SqliteConnection Connection => _connection;
    
    public async Task InitializeAsync()
    {
        await OpenConnection();
        await InitializeCustomersTable(Connection);
    }

    public async Task DisposeAsync()
    {
        if (Connection != null)
            await Connection.DisposeAsync();
        else
            throw new ObjectDisposedException(nameof(InMemoryCustomerDb));
    }

    public async Task SeedCustomerAsync()
    {
        await using var insertCommand =
            InsertCommand(new("SomeName", "exampleEmail@exampleEmail"), "passwordHash");
        
        await insertCommand.ExecuteNonQueryAsync();
    }
    
    public async Task SeedCustomerAsync(Customer customer, string passwordHash)
    {
        await using var insertCommand = InsertCommand(customer, passwordHash);
        
        await insertCommand.ExecuteNonQueryAsync();
    }

    protected override async Task<SqliteConnection> OpenConnection()
    {
        _connection = new SqliteConnection("Filename=:memory:;Cache=Shared");
        await Connection.OpenAsync();
        return await Task.FromResult(Connection);
    }

    private SqliteCommand InsertCommand(Customer customer, string passwordHash)
    {
        var insertCommand = new SqliteCommand("""
                                              INSERT INTO CUSTOMERS 
                                              (Id, Name, SubscriptionType, Email, PasswordHash) 
                                              VALUES (@id, @name, @type, @email, @passwordHash)
                                              """, Connection);
        
        insertCommand.Parameters.AddWithValue("@id", customer.Id);
        insertCommand.Parameters.AddWithValue("@name", customer.Name);
        insertCommand.Parameters.AddWithValue("@type", customer.SubscriptionType);
        insertCommand.Parameters.AddWithValue("@email", customer.Email);
        insertCommand.Parameters.AddWithValue("@passwordHash", passwordHash);
        
        return insertCommand;
    }
}

public class InMemoryCustomerDbTests : IAsyncLifetime
{
    private readonly InMemoryCustomerDb _inMemoryDb = new();
    
    public async Task InitializeAsync()
    {
        await _inMemoryDb.InitializeAsync();
    }

    public Task DisposeAsync()
    {
        return _inMemoryDb.DisposeAsync();
    }
    
    [Fact]
    public async Task InMemoryCustomerDb_Initialize_Test()
    {
        await _inMemoryDb.InitializeAsync();
        await _inMemoryDb.SeedCustomerAsync();
        
        var checkCmd = new SqliteCommand("SELECT COUNT(*) FROM Customers", _inMemoryDb.Connection);
        var count = (long)await checkCmd.ExecuteScalarAsync();
        
        Assert.Equal(1, count);
    }
}