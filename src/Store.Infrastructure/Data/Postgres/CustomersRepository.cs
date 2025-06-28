using Store.Core.Exceptions;
using Store.Core.Contracts.Repositories;
using Store.Core.Models;
using Microsoft.EntityFrameworkCore;
using Store.Infrastructure.Mappers;

namespace Store.Infrastructure.Data.Postgres;


public class CustomersRepository : ICustomerRepository
{
    private readonly AppDbContext _db;

    public CustomersRepository(AppDbContext db) => _db = db;

    public async Task RegisterAsync(Customer customer, string passwordHash)
    {
        await _db.Customers.AddAsync(CustomerMapper.ToEntity(customer, passwordHash));
        await _db.SaveChangesAsync();
    }

    public async Task<bool> IsEmailClaimed(string email) =>
        await _db.Customers
            .AsNoTracking()
            .AnyAsync(c => c.Email == email);

    public async Task<(Customer customer, string passwordHash)> GetCustomerDataByEmail(string email)
    {
        var customerEntity = await _db.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Email == email)
            ?? throw new CustomerNotFound($"Customer with email {email} not found");
        return (CustomerMapper.ToDomain(customerEntity), customerEntity.PasswordHash);
    }

    public async Task DeleteAsync(Guid id)
    {
        var customer =  await _db.Customers .FindAsync(id)
            ?? throw new CustomerNotFound($"Customer with id {id} not found");
        
        _db.Customers.Remove(customer);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> IsCustomerExistsAsync(Guid id) => await _db.Customers.AsNoTracking().AnyAsync(c => c.Id == id);
}