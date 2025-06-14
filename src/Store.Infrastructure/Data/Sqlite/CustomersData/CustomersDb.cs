using Microsoft.Data.Sqlite;

namespace Store.Infrastructure.Data.Sqlite.CustomersData;

internal abstract class CustomersDb
{
    private static readonly string FileName = "customers.db";
    private static readonly string DbPath = Path.Combine(Directory.GetCurrentDirectory(), FileName);

    public async Task InitializeDatabaseAsync()
    {
        try
        {
            if (!File.Exists(DbPath))
                File.Create(DbPath).Close();

            await using var db = await OpenConnection();

            await InitializeCustomersTable(db);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    protected async Task InitializeCustomersTable(SqliteConnection db)
    {
        await using var createTable = new SqliteCommand("""
                                                        CREATE TABLE IF NOT EXISTS Customers (
                                                        Id TEXT PRIMARY KEY,
                                                        Name NVARCHAR(50),
                                                        SubscriptionType NVARCHAR(20),
                                                        Email NVARCHAR(255),
                                                        PasswordHash TEXT
                                                        )
                                                        """, db);

        await createTable.ExecuteNonQueryAsync();
    }
    
    protected virtual async Task<SqliteConnection> OpenConnection()
    {
        var connection = new SqliteConnection($"Filename={DbPath}");
        await connection.OpenAsync();
        return connection;
    }
}