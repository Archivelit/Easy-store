namespace Store.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ItemEntity> Items { get; set; }

    private readonly string? _connectionString;

    public AppDbContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Log.Debug("Configuring DbContext");
        optionsBuilder
            .UseNpgsql(_connectionString)
            .EnableSensitiveDataLogging()
            .UseSeeding((context, _) =>
            {
                Log.Logger.Debug("Seeding database started");

                if (context is AppDbContext appContext)
                {
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
                    await new UserSeeder().SeedUsersAsync(appContext, ct);
                    await new ItemSeeder().SeedItemsAsync(appContext, ct);
                }

                Log.Logger.Debug ("Seeding database completed");
            });
        Log.Debug("DbContext configured succesfuly");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        Log.Debug("Database model created");
    }
}