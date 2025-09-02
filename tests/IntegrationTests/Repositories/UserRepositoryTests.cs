using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Store.Core.Contracts.Repositories;
using Store.Core.Models;
using Store.Infrastructure.Data;

namespace IntegrationTests.Repositories;

public class UserRepositoryTests : IClassFixture<ApiFixture>
{
    private readonly IUserRepository _userRepository;
    private readonly AppDbContext _context;
    
    public UserRepositoryTests(ApiFixture fixture)
    {
        var scope = fixture.Factory.Services.CreateScope();
        _userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    }

    [Fact]
    public async Task CreateUser_ShouldWork()
    {
        // Arrange
        var username = "Bob";
        var userEmail = "demouser@domain.com";
        
        var user = new User(username, userEmail);
        
        // Act
        await _userRepository.RegisterAsync(user);
        
        // Assert
        var saved = await _context.Users.FirstOrDefaultAsync(u => u.Email == "bob@example.com", cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(saved);
        Assert.Equal("Bob", saved.Name);
    }
}