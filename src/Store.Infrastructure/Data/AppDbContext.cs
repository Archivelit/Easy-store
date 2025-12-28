using Microsoft.EntityFrameworkCore.Design;
using System.Threading.Tasks.Dataflow;

namespace Store.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ItemEntity> Items { get; set; }
    public DbSet<UserCredentialsEntity> UserCredentials { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Log.Debug("Configuring DbContext");
        optionsBuilder
            .EnableSensitiveDataLogging()
            .UseSeeding((context, _) =>
            {   
                Log.Logger.Debug("Seeding database started");

                if (context is AppDbContext appContext)
                {
                    // TODO: Add user credentials seeding
                    new UserSeeder().SeedUsers(appContext);
                    new ItemSeeder().SeedItems(appContext);
                }

                Log.Logger.Debug("Seeding database completed");
            })
            .UseAsyncSeeding(async (context, _, ct) =>
            {
                Log.Logger.Debug("Seeding database started");

                if (context is AppDbContext appContext)
                {
                    // TODO: Add user credentials seeding
                    await new UserSeeder().SeedUsersAsync(appContext, ct);
                    await new ItemSeeder().SeedItemsAsync(appContext, ct);
                }

                Log.Logger.Debug("Seeding database completed");
            });
        Log.Debug("DbContext configured succesfuly");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        Log.Debug("Database model created");
    }
}

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    private readonly string _configurationPath = Path.Combine([Directory.GetCurrentDirectory(), "..", "Store.API"]);

    public AppDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(_configurationPath)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("PostgreSql connection string is not configured.");

        optionsBuilder.UseNpgsql(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}