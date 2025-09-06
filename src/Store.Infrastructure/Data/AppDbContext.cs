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
            .EnableSensitiveDataLogging();
        Log.Debug("DbContext configured succesfuly");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        Log.Debug("Database model created");
    }
}