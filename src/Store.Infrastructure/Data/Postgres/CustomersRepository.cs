using Store.Core.Exceptions;
using Store.Core.Contracts.Repositories;
using Store.Core.Models;
using Microsoft.EntityFrameworkCore;
using Store.Infrastructure.Mappers;
using Microsoft.Extensions.Logging;

namespace Store.Infrastructure.Data.Postgres;


public class CustomersRepository : ICustomerRepository
{
    private readonly AppDbContext _db;
    private readonly ILogger<CustomersRepository> _logger;

    public CustomersRepository(AppDbContext db, ILogger<CustomersRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task RegisterAsync(Customer customer, string passwordHash)
    {
        _logger.LogInformation("Registering user {UserId}", customer.Id);

        await _db.Customers.AddAsync(CustomerMapper.ToEntity(customer, passwordHash));
        await _db.SaveChangesAsync();

        _logger.LogInformation("User {UserId} registered succesfuly", customer.Id);
    }

    public async Task<bool> IsEmailClaimedAsync(string email) =>
        await _db.Customers
            .AsNoTracking()
            .AnyAsync(c => c.Email == email);

    public async Task<(Customer customer, string passwordHash)> GetByEmailAsync(string email)
    {
        var customerEntity = await _db.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Email == email)
            ?? throw new CustomerNotFound($"Customer with email {email} not found");
        return (CustomerMapper.ToDomain(customerEntity), customerEntity.PasswordHash);
    }

    public async Task DeleteAsync(Guid id)
    {
        var customer = await _db.Customers.FindAsync(id)
            ?? throw new CustomerNotFound($"Customer with id {id} not found");

        _db.Customers.Remove(customer);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> IsExistsAsync(Guid id) => await _db.Customers.AsNoTracking().AnyAsync(c => c.Id == id);

    public async Task<(Customer customer, string passwordHash)> GetByIdAsync(Guid id)
    {
        var entity = await _db.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        
        if (entity == null)
            throw new CustomerNotFound($"Customer with id {id} not found");
        
        return (CustomerMapper.ToDomain(entity), entity.PasswordHash);
    }
    
    public async Task UpdateAsync(Customer customer, string passwordHash)
    {
        var entity = CustomerMapper.ToEntity(customer, passwordHash);
        
        _db.Customers.Update(entity);
        await _db.SaveChangesAsync();
    }
}