using Microsoft.EntityFrameworkCore;
using Store.Core.Models;

namespace API.Data;

public class CustomersDbContext : DbContext
{
    public CustomersDbContext(DbContextOptions<CustomersDbContext> options) : base (options){}
    
    public DbSet<Customer> Customers { get; set; }
}