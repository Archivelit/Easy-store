using Microsoft.EntityFrameworkCore;

namespace IntegrationTests.Handlers.User.Command;

public class UpdateUserCommandHandlerTests : IClassFixture<StoreApiFixture>
{
    private readonly StoreApiFixture _fixture;
    private readonly IServiceScope _scope;
    
    public UpdateUserCommandHandlerTests(StoreApiFixture fixture)
    {
        _fixture = fixture;
        _scope = _fixture.Services.CreateScope();
    }

    [Fact]
    public async Task UpdateUser_ShouldWork() 
    {
        // Arrange
        var expectedUser = new UserDto(SeedModels.User2) with { Name = "John Doe" };
        var handler = _scope.ServiceProvider.GetRequiredService<ICommandHandler<UpdateUserCommand>>();
        var command = new UpdateUserCommand(expectedUser);
        var db = _scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var userFromDb = db.Users.FirstOrDefault(u => u.Email == SeedModels.User2.Email);
        userFromDb.Should().BeEquivalentTo(expectedUser);
    }
}