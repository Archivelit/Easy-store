using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Store.Infrastructure.Entities;

namespace Store.Infrastructure.Data.Postgres;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    private readonly string? _connectionString;

    public AppDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Log.Debug("Configuring DbContext");
        optionsBuilder
            .UseNpgsql(_connectionString)
            .EnableSensitiveDataLogging();
        Log.Debug("DbContext configured succesfuly");
    }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ItemEntity> Items { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        Log.Debug("Database model created");
    }
}