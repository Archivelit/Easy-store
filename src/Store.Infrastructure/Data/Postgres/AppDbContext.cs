using Microsoft.EntityFrameworkCore;
using Store.Infrastructure.Entities;

namespace Store.Infrastructure.Data.Postgres;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base (options){}
    
    public DbSet<CustomerEntity> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
